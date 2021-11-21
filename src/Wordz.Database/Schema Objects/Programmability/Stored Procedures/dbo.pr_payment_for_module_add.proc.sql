-- Description: Внесение оплаты за модуль
-- Change Date: 01.06.2013

CREATE PROCEDURE [dbo].[pr_payment_for_module_add]
	@account_id int, 
	@module_id int,
	@payment_date datetime,
	@currency_id int,
	@payment_value decimal(18, 2),
	@discount_rate decimal(18, 8),
	@result_id int output
AS
	declare @tranName varchar(max)
	set @tranName = 'myTranactionModule'

	begin transaction @tranName;
	begin try	
		
		if (not exists(select pt.id
					from dbo.payment pt
					where pt.module_id = @module_id and pt.account_id = @account_id))
		begin
			declare @price decimal(18, 2);
			declare @module_currency_id int;
			
			select @price = m.price,
				@module_currency_id = m.currency_id
			from dbo.module m
			where m.id = @module_id

			if (@discount_rate <> 1.0)
			begin
				select @price = @price * @discount_rate;
			end

			declare @payments table (currency_id int, value decimal(18, 2));
			declare @balance table (currency_id int, value decimal(18, 2));

			insert into @balance
			select acb.currency_id, acb.value
			from dbo.account_money_balance acb
			where acb.account_id = @account_id
			
			declare @currentValue decimal(18, 2);
			declare @currentCurrency_id int;

			-- Сперва берем из той валюты, которая проставлена в модуле
			select @currentCurrency_id = currency_id,
				@currentValue = value
			from @balance
			where currency_id = @module_currency_id
			
			delete from @balance
			where currency_id = @module_currency_id

			-- Если нет валюты, требуемой для модуля, то просто берем любую
			if (@currentCurrency_id is null)
			begin
				select top 1 @currentCurrency_id = currency_id,
					@currentValue = value
				from @balance
			
				delete from @balance
				where currency_id = @currentCurrency_id
			end

			while (@currentCurrency_id is not null and @price > 0)
			begin
				if (@currentCurrency_id is not null 
					and @currentValue is not null 
					and @currentValue > 0)
				begin
					-- Получаем множитель для преобразования валют
					declare @currencyRateMultiplier decimal(18, 8);
					select @currencyRateMultiplier = dbo.fn_get_currency_rate(@module_currency_id, @currentCurrency_id, @payment_date);

					if (@currencyRateMultiplier is null)
					begin
						raiserror('There is not currency rate!', 16, 1);
					end

					-- тут есть множитель для преобразования
					-- вычисляем количество денег
					declare @neededSum decimal(18, 2);
					set @neededSum = @price * @currencyRateMultiplier;
					if (@currentValue >= @neededSum)
					begin
						set @price = 0;
						insert into @payments (currency_id, value)
							values(@currentCurrency_id, @neededSum)
					end
					else
					begin
						set @price = @price - (@currentValue / @currencyRateMultiplier);
						insert into @payments (currency_id, value)
							values(@currentCurrency_id, @currentValue / @currencyRateMultiplier);
					end
				end
				
				set @currentCurrency_id = null;
				-- выбираем запас для следующей валюты
				select top 1 @currentCurrency_id = currency_id,
					@currentValue = value
				from @balance
			
				delete from @balance
				where currency_id = @currentCurrency_id
			end

			if (@price <> 0)
			begin
				raiserror('Not enought of money!', 16, 1);
			end
			
			insert into dbo.payment
				(account_id, module_id, currency_id, payment_date, payment_value)
			values
				(@account_id, @module_id, @currency_id, @payment_date, @payment_value)
			
			select @result_id = SCOPE_IDENTITY()  -- Курс успешно оплачен

			insert into dbo.account_money_hystory
				(account_id, change_date, currency_id, income_value)
			select @account_id, @payment_date, p.currency_id, -p.value
			from @payments p
		end
		else
		begin
			select @result_id = -1 -- Оплата по курсу уже внесена
		end

		commit transaction @tranName;
	end try
	begin catch
		rollback transaction @tranName;
		select @result_id = -2;
	end catch
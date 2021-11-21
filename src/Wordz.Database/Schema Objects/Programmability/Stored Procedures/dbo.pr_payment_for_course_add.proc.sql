-- Description: Внесение оплаты за весь курс
-- Change Date: 01.06.2013

CREATE PROCEDURE [dbo].[pr_payment_for_course_add]
	@account_id int, 
	@course_id int,
	@payment_date datetime,
	@currency_id int,
	@payment_value decimal(18, 2),
	@result_id int output
AS
	declare @tranName varchar(max)
	set @tranName = 'myTransactionCourse';
	
	begin try
	begin transaction @tranName
		
		declare @moduleForPayment table(id int, price decimal(18,2), currency_id int)

		insert into @moduleForPayment
		select m.id, m.price, m.currency_id 
		from dbo.module m
		where m.course_id = @course_id
			and m.parent_id is null
			and not exists (select pt.id 
							from dbo.payment pt
							where pt.module_id = m.id and pt.account_id = @account_id)
			

		declare @courseCurrency int;
		declare @courseDefaultPrice decimal(18, 2);
		select @courseCurrency = currency_id,
			@courseDefaultPrice = price
		from dbo.course
		where id = @course_id

		if (exists(select id from @moduleForPayment))
		begin
			
			-- рассчитываем коэффициент скидки
			declare @sum_for_all_modules decimal(18,2)

			-- сумма по всем модулям в валюте курса
			select @sum_for_all_modules = 
				SUM(case
						when m.currency_id = @courseCurrency then m.price
						else m.price * ISNULL(dbo.fn_get_currency_rate(m.currency_id, @courseCurrency, @payment_date), 1.0)
					end)
			from dbo.module m
			where m.course_id = @course_id
				and m.parent_id is null
						
			declare @discount_rate decimal (18, 8);
			select @discount_rate = @courseDefaultPrice / @sum_for_all_modules;

			-- оплачиваем все модули

			declare @cursor_id int;
			declare @cursor_price decimal(18,2);
			declare @cursor_currency_id int;
			declare @result_for_module int;

			declare module_corsor cursor for
			select * 
			from @moduleForPayment

			open module_corsor
			
			fetch next from module_corsor into
			@cursor_id, 
			@cursor_price, 
			@cursor_currency_id
			
			while @@FETCH_STATUS = 0
			begin
				-- вычисляем стоимость курса с расчетом на скидку
				select @result_for_module = 0;
			
				exec [dbo].[pr_payment_for_module_add] 
					@account_id = @account_id, 
					@module_id = @cursor_id,
					@payment_date = @payment_date,
					@currency_id = @cursor_currency_id,
					@payment_value = @cursor_price,
					@discount_rate = @discount_rate,
					@result_id = @result_for_module output;

				if (@result_for_module < 0)
				begin
					raiserror('Cannot pay for module or already paid!', 16, 1);
				end

				fetch next from module_corsor into
				@cursor_id, 
				@cursor_price, 
				@cursor_currency_id
			end

			close module_corsor
			deallocate module_corsor

			select @result_id = @result_for_module
		end
		else
		begin
			select @result_id = -1 -- Оплата по курсу уже внесена
		end

		commit transaction @tranName
		select @result_id
	end try
	begin catch
		rollback transaction @tranName
		select @result_id = -2
		print ERROR_MESSAGE()
	end catch
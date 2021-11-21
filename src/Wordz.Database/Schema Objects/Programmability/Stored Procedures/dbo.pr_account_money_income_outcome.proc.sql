-- description: хранимка для пополнения средств на счете и сохранения 
--      в истории прихода / расхода
-- change date: 30-05-2013

CREATE PROCEDURE [dbo].[pr_account_money_income_outcome]
	@account_id int,
	@currency_id int,
	@value decimal(18, 2),
	@payment_date datetime,
	@result_id int
AS
	begin try
		begin transaction 

		insert into dbo.account_money_hystory
			(account_id, currency_id, income_value, change_date)
		values (@account_id, @currency_id, @value, @payment_date)

		select @result_id = SCOPE_IDENTITY();
		commit
	end try
	begin catch
		rollback
		select @result_id = -1
	end catch
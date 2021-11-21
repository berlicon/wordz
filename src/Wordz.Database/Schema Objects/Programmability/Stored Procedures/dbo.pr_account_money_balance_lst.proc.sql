-- возвращает денежный баланс по каждой валюте
CREATE PROCEDURE [dbo].[pr_account_money_balance_lst]
	@account_id int
AS
	select 
		amb.currency_id 'currency_id'
		, amb.value 'value'
		, amb.last_update 'last_update'
		, cs.name 'currency_name'
		, cs.letter_code 'letter_code'
		, cs.digit_code 'digit_code'
	from dbo.account_money_balance amb
	left join dbo.currency cs
	on cs.id = amb.currency_id
	where amb.account_id = @account_id
		and amb.value <> 0
	
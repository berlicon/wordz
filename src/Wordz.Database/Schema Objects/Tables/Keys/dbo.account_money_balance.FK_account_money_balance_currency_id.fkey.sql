ALTER TABLE [dbo].[account_money_balance]
	ADD CONSTRAINT [FK_account_money_balance_currency_id] 
	FOREIGN KEY (currency_id)
	REFERENCES dbo.currency (id)	


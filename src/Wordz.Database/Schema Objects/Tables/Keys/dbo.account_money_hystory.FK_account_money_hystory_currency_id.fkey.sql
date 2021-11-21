ALTER TABLE [dbo].[account_money_hystory]
	ADD CONSTRAINT [FK_account_money_hystory_currency_id] 
	FOREIGN KEY (currency_id)
	REFERENCES currency (id)	


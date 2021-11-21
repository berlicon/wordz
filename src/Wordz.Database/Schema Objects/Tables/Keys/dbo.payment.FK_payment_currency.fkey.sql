ALTER TABLE [dbo].[payment]
	ADD CONSTRAINT [FK_payment_currency] 
	FOREIGN KEY (currency_id)
	REFERENCES dbo.currency (id)	


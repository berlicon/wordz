ALTER TABLE [dbo].[currency_rate]
	ADD CONSTRAINT [FK_currency_rate_target_currency_id] 
	FOREIGN KEY (target_currency_id)
	REFERENCES currency (id)	


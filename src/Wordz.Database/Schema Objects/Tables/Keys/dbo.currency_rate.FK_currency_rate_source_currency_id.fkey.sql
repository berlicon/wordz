ALTER TABLE [dbo].[currency_rate]
	ADD CONSTRAINT [FK_currency_rate_source_currency_id] 
	FOREIGN KEY (source_currency_id)
	REFERENCES currency (id)	


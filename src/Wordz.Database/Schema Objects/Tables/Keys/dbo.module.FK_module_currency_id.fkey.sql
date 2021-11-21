ALTER TABLE [dbo].[module]
	ADD CONSTRAINT [FK_module_currency_id]
	FOREIGN KEY (currency_id)
	REFERENCES currency (id)

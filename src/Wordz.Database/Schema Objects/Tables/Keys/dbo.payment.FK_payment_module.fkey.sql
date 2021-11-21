ALTER TABLE [dbo].[payment]
	ADD CONSTRAINT [FK_payment_module] 
	FOREIGN KEY (module_id)
	REFERENCES [dbo].[module] (id)


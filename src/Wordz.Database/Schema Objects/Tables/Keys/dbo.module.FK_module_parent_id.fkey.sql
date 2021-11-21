ALTER TABLE [dbo].[module]
	ADD CONSTRAINT [FK_module_parent_id] 
	FOREIGN KEY (parent_id)
	REFERENCES dbo.module (id)	


ALTER TABLE [dbo].[module]
	ADD CONSTRAINT [FK_module_picture] 
	FOREIGN KEY (picture_id)
	REFERENCES picture (id)	


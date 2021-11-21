ALTER TABLE [dbo].[course]
	ADD CONSTRAINT [FK_course_account] 
	foreign key ([owner_id]) references [account](id)


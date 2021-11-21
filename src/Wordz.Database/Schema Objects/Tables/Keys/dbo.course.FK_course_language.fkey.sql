ALTER TABLE [dbo].[course]
	ADD CONSTRAINT [FK_course_language] 
	foreign key ([ui_langauge_id]) references [language](id)
	on update no action 
	on delete no action


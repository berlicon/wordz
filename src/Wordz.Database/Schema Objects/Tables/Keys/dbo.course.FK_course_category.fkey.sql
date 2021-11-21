ALTER TABLE [dbo].[course]
	ADD CONSTRAINT [FK_course_category] 
	foreign key ([category_id]) references [category](id)
	on update no action 
	on delete no action


ALTER TABLE [dbo].[module]
	ADD CONSTRAINT [FK_module_course] 
	foreign key ([course_id]) references [course](id)
	on update no action 
	on delete no action


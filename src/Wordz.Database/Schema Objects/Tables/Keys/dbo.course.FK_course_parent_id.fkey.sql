ALTER TABLE [dbo].[course]
	ADD CONSTRAINT [FK_course_parent_id] 
	FOREIGN KEY (parent_id)
	REFERENCES course (id)	


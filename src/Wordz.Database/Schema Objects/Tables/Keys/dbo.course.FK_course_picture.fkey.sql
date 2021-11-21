ALTER TABLE [dbo].[course]
	ADD CONSTRAINT [FK_course_picture] 
	FOREIGN KEY (picture_id)
	REFERENCES picture (id)	


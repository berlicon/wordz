ALTER TABLE [dbo].[course]
	ADD CONSTRAINT [FK_course_currency_id] 
	FOREIGN KEY (currency_id)
	REFERENCES currency (id)	

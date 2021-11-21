ALTER TABLE [dbo].[exercise_text]
	ADD CONSTRAINT [FK_exercise_text_module] 
	FOREIGN KEY (module_id)
	REFERENCES module (id)	


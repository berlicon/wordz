CREATE TABLE [dbo].[user_password_for_course]
(
	[user_id] int NOT NULL, 
	[course_id] int NOT NULL,
	[password] nvarchar(100)
)

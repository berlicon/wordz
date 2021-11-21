-- Change Date: 2013.05.21
-- Description: таблица комментариев

CREATE TABLE [dbo].[user_comment]
(
	id int identity(1, 1) NOT NULL, 
	comment_text nvarchar(500) NULL,
	account_id int NOT NULL,
	target_element uniqueidentifier NOT NULL,
	created_date datetime NOT NULL,
	rating int NOT NULL default 0,
	claims_count int NOT NULL default 0,
	answer_level int NOT NULL default 0
)

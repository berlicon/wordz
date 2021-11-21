CREATE TABLE [dbo].[rating]
(
	target_element uniqueidentifier NOT NULL, 
	account_id int NULL,
	value float NOT NULL
)

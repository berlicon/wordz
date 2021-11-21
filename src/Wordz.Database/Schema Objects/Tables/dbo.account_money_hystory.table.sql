CREATE TABLE [dbo].[account_money_hystory]
(
	id int identity(1, 1) NOT NULL,
	account_id int NOT NULL,
	change_date datetime NOT NULL,
	income_value decimal(18, 2) NOT NULL,
	currency_id int NOT NULL,
	[description] nvarchar(4000) NULL
)

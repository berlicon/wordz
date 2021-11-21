CREATE TABLE [dbo].[account_money_balance]
(
	id int identity(1, 1) NOT NULL, 
	account_id int NOT NULL,
	currency_id int NOT NULL,
	value decimal(18, 2) NOT NULL,
	last_update datetime NOT NULL
)

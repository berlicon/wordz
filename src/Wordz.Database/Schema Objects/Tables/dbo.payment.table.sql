CREATE TABLE [dbo].[payment]
(
	id int identity(1,1) NOT NULL,
	account_id int NOT NULL,
	module_id int NULL,
	payment_value decimal(18, 2) NOT NULL,
	currency_id int NOT NULL,
	payment_date datetime NOT NULL
)

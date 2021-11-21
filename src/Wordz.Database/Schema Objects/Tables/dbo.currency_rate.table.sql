CREATE TABLE [dbo].[currency_rate]
(
	id int identity(1, 1) NOT NULL,
	source_currency_id int NOT NULL,
	target_currency_id int NOT NULL,
	multiplier decimal(18, 8) NOT NULL,
	change_date datetime NOT NULL
)

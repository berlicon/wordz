CREATE TABLE [dbo].[currency]
(
	id int identity(1,1) NOT NULL, 
	name varchar(100) NOT NULL,
	-- формат и коды валют по стандарту ISO 4217
	letter_code varchar(3) NOT NULL,
	digit_code int NOT NULL
)

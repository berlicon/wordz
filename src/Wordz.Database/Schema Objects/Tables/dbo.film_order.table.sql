CREATE TABLE [dbo].[film_order]
(
	id int identity(1,1) NOT NULL, 
	film_id int NOT NULL,
	account_id int NOT NULL,
	[order_in_list] int not null default(0)
)

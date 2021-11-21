CREATE TABLE [dbo].[tv_order]
(
	id int identity(1,1) NOT NULL, 
	tv_id int NOT NULL,
	account_id int NOT NULL,
	[order_in_list] int not null default(0)
)

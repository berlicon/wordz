CREATE TABLE [dbo].[user_comment_rates_and_claims]
(
	[id] int identity(1, 1) NOT NULL, 
	[user_comment_id] int NOT NULL,
	[account_id] int NOT NULL,
	[is_claimed] bit,
	[is_rated] bit,
	[is_positive_rate] bit
)

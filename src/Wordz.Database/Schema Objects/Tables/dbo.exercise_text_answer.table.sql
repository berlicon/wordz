CREATE TABLE [dbo].[exercise_text_answer]
(
	[id] int identity(1,1) not null,
	[exercise_id] int not null,
	[account_id] int not null,
	[text] nvarchar(max) not null,
	[mark] int null
)

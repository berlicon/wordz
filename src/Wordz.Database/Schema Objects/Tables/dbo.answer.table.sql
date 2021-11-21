CREATE TABLE [dbo].[answer]
(
	[id] int identity(1,1) not null,
	[exercise_id] int not null,
	[text] nvarchar(max) not null,
	[picture_id] int null,
	[is_right] bit not null default 0,
	[is_approved] bit not null default 1,
	[parent_id] int null
)

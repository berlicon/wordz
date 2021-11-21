CREATE TABLE [dbo].[exercise_text]
(
	[id] int identity(1,1) not null,
	[module_id] int not null,
	[name] nvarchar(200) not null,
	[description] nvarchar(max) not null,
	[text] nvarchar(max) not null,
	[ordinal_number] int not null default 1,
	[is_approved] bit not null default 1,
	[parent_id] int null
)

CREATE TABLE [dbo].[module]
(
		[id] int identity(1,1) not null,
    	[number] uniqueidentifier not null,
    	[course_id] int not null,
        [name] nvarchar(100) not null,
        [description] nvarchar(300) null,
        [detailed_description] ntext null,
        [picture_id] int null,
        [price] decimal(18,2) null,
		[currency_id] int null,
        [url] varchar(100) null,
        [tags] nvarchar(1000) null,
        [links] nvarchar (4000) null,
		[order_in_course] int not null default 0,
		[parent_id] int null,
		[exercise_max_number] int not null default 0,
		[is_approved] bit,
		[is_deleted] bit default(0)
)

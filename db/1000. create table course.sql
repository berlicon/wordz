-- Author: Trenogina E.
-- Create time: 18.05.2013
-- Create table of courses
-- ======================================================================

-- table 
if not exists (select * from sysobjects where name='course' and xtype='U')
    create table course (
		[id] int identity(1,1) not null,
        [number] uniqueidentifier not null,
        [name] nvarchar(100) not null,
        [description] nvarchar(300) null,
        [detailed_description] ntext null,
        [logotype] varbinary(max) null,
        [price] decimal(18,2) null,
        [ui_langauge_id] int not null,
        [location_id] int null,
        [category_id] int null,
        [url] varchar(100) null,
        [authors] nvarchar(1000) null,
        [contacts] nvarchar(1000) null,
        [tags] nvarchar(1000) null,
        [links] nvarchar (4000) null,
        [is_editable] bit not null,
        [is_copied] bit not null,
        [is_public] bit not null,
        [parol] nvarchar(100) null,
        [google_advertisement] nvarchar(max),
        [is_approved] bit not null,        
        constraint [PK_course] primary key clustered 
        (
			[id] ASC
		)
	)
go

--constraints
if not exists (select * from sysobjects where xtype = 'F' AND name = 'FK_course_language')
alter table course 
add constraint [FK_course_language] foreign key ([ui_langauge_id]) references [language](id)
	on update no action 
	on delete no action
go

if not exists (select * from sysobjects where xtype = 'F' AND name = 'FK_course_category')
alter table course 
add constraint [FK_course_category] foreign key ([category_id]) references [category](id)
	on update no action 
	on delete no action
go

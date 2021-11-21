-- Author: Trenogina E.
-- Create time: 18.05.2013
-- Create table of module
-- ======================================================================

-- table 
if not exists (select * from sysobjects where name='module' and xtype='U')
    create table module (
    	[id] int identity(1,1) not null,
    	[number] uniqueidentifier not null,
    	[course_id] int not null,
        [name] nvarchar(100) not null,
        [description] nvarchar(300) null,
        [detailed_description] ntext null,
        [logotype] varbinary(max) null,
        [price] decimal(18,2) null,
        [url] varchar(100) null,
        [tags] nvarchar(1000) null,
        [links] nvarchar (4000) null,
        constraint [PK_module] primary key clustered
        (
			[id] ASC
		)
    )
go

--constraint
if not exists (select * from sysobjects where xtype = 'F' AND name = 'FK_module_course')
alter table module 
add constraint [FK_module_course] foreign key ([course_id]) references [course](id)
	on update no action 
	on delete no action
go

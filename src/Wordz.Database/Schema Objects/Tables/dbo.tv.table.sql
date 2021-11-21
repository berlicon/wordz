CREATE TABLE [dbo].[tv] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [image_url]   NVARCHAR (50)   NULL,
    [url]         NVARCHAR (2000) NOT NULL,
    [language_id] INT             NOT NULL,
	[account_id] int null,
	[is_editable] bit not null default(0) 
);


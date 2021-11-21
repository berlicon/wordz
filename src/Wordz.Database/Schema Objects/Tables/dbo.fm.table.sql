CREATE TABLE [dbo].[fm] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [image_url]        NVARCHAR (50)  NULL,
    [url]              NVARCHAR (500) NOT NULL,
    [use_media_player] BIT            NOT NULL,
    [language_id]      INT            NOT NULL,
	[is_editable] bit not null default(0),
	[account_id] int null,
);


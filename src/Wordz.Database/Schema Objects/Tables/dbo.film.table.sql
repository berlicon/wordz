CREATE TABLE [dbo].[film] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [name]             NVARCHAR (100) NOT NULL,
	[description]      NVARCHAR (500) NULL,
	[image_url]        NVARCHAR (50)  NULL,
    [url]              NVARCHAR (100) NULL,
    [film_category_id] INT            NOT NULL,
    [film_player_id]   INT            NULL,
    [language_id]      INT            NOT NULL,
    [status]           BIT            NULL,
	[is_editable]      BIT            NUll,
	[account_id]       INT            NULL,
	[player_code]      NVARCHAR(3000) NULL
);


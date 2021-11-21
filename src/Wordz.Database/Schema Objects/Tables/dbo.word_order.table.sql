CREATE TABLE [dbo].[word_order] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [sentence]    NVARCHAR (150) NOT NULL,
    [language_id] INT            NOT NULL
);


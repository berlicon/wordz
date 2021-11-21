CREATE TABLE [dbo].[level] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [sentence]    NVARCHAR (150) NOT NULL,
    [answer1]     NVARCHAR (50)  NOT NULL,
    [answer2]     NVARCHAR (50)  NOT NULL,
    [answer3]     NVARCHAR (50)  NOT NULL,
    [correct]     TINYINT        NOT NULL,
    [language_id] INT            NOT NULL
);


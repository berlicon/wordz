CREATE TABLE [dbo].[word_quiz] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [account_id]  INT           NULL,
    [nick]        NVARCHAR (20) NULL,
    [result]      TINYINT       NOT NULL,
    [language_id] INT           NOT NULL
);


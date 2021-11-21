CREATE TABLE [dbo].[article] (
    [id]                 INT           IDENTITY (1, 1) NOT NULL,
    [title]              VARCHAR (100) NOT NULL,
    [body]               TEXT          NOT NULL,
    [category_id]        INT           NULL,
    [site_id]            INT           NULL,
    [order]              INT           NOT NULL,
    [native_language_id] INT           NOT NULL,
    [learn_language_id]  INT           NOT NULL
);


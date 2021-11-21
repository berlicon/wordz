CREATE TABLE [dbo].[verb] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [form1]        VARCHAR (40)  NOT NULL,
    [form2]        VARCHAR (40)  NOT NULL,
    [form3]        VARCHAR (40)  NOT NULL,
    [translation]  VARCHAR (256) NOT NULL,
    [popular]      BIT           NOT NULL,
    [verb_type_id] INT           NOT NULL
);


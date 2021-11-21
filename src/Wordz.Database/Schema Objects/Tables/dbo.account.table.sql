CREATE TABLE [dbo].[account] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [nick]     NVARCHAR (20) NOT NULL,
    [email]    NVARCHAR (50) NULL,
    [password] NVARCHAR (20) NULL,
	[is_admin] bit NULL
);


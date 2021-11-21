
CREATE PROCEDURE dbo.pr_account_ins
(
    @nick nvarchar(20),
    @email nvarchar(50) = NULL,
    @password nvarchar(20) = NULL
)
AS

INSERT INTO account
(nick, email, password)
VALUES (@nick, @email, @password)

SELECT SCOPE_IDENTITY()


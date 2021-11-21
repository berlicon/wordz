
CREATE PROCEDURE dbo.pr_account_get
(
    @nick nvarchar(20),
    @password nvarchar(20) = NULL
)
AS

SELECT id, nick, email, password, is_admin
FROM account
WHERE nick = @nick
AND (password IS NULL OR password = @password) 


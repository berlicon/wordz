
CREATE PROCEDURE dbo.pr_account_upd
(
    @id int,
    @nick nvarchar(20),
    @email nvarchar(50) = NULL,
    @password nvarchar(20) = NULL
)
AS

UPDATE account
SET nick = @nick, email = @email, password = @password
WHERE id=@id


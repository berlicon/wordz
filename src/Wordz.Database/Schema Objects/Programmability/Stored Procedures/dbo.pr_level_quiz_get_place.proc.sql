CREATE PROCEDURE pr_level_quiz_get_place
(
    @account_id int = null,
    @nick nvarchar(20),
    @success_count int,
    @language_id int
)
AS

IF ((SELECT COUNT(*)  FROM level_quiz) >= 100) DELETE FROM level_quiz

INSERT INTO level_quiz(account_id, nick, result, language_id)
VALUES(@account_id, @nick, @success_count, @language_id)

DECLARE @id int
SET @id = (SELECT SCOPE_IDENTITY())

DECLARE @places TABLE
(
    place int IDENTITY (1, 1) NOT NULL,
    id int,
    account_id int,
    nick nvarchar(20),
    result int,
    language_id int
)

INSERT INTO @places (id, account_id, nick, result, language_id)
SELECT id, account_id, nick, result, language_id
FROM level_quiz
WHERE language_id = @language_id
ORDER BY result DESC, id DESC

SELECT TOP 1 place
FROM @places
WHERE id = @id

DELETE lq
FROM level_quiz lq
INNER JOIN @places p
ON lq.id = p.id
WHERE p.place > 100 AND lq.language_id = @language_id
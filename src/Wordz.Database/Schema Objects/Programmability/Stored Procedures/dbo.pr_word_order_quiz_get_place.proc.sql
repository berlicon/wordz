CREATE PROCEDURE pr_word_order_quiz_get_place
(
    @account_id int = null,
    @nick nvarchar(20),
    @success_count int,
    @language_id int
)
AS

IF ((SELECT COUNT(*)  FROM word_order_quiz) >= 100) DELETE FROM word_order_quiz

INSERT INTO word_order_quiz(account_id, nick, result, language_id)
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
FROM word_order_quiz
WHERE language_id = @language_id
ORDER BY result DESC, id DESC

SELECT TOP 1 place
FROM @places
WHERE id = @id

DELETE wq
FROM word_order_quiz wq
INNER JOIN @places p
ON wq.id = p.id
WHERE p.place > 100 AND wq.language_id = @language_id


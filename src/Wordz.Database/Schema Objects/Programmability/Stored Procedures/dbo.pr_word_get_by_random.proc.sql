
CREATE PROCEDURE dbo.pr_word_get_by_random
(
    @account_id int,
    @word_count int,
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word TABLE
(
    number int IDENTITY (1, 1) NOT NULL,
    id int
)

INSERT INTO @word (id)
SELECT w.id
FROM word w
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @learn_language_id
INNER JOIN word_language wl2
ON w.id = wl2.word_id AND wl2.language_id = @native_language_id
WHERE w.id <= 18630 AND (w.id NOT IN
    (SELECT word_id
     FROM account_word
     WHERE account_id = @account_id AND
     language_id = @learn_language_id))
ORDER BY NEWID()

SELECT ww.word_id AS id, ww.text AS original, www.text AS [translation]
FROM @word w
INNER JOIN word_language ww
ON w.id = ww.word_id AND ww.language_id = @learn_language_id
INNER JOIN word_language www
ON w.id = www.word_id AND www.language_id = @native_language_id
AND w.number <= @word_count
ORDER BY w.number


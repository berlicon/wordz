
CREATE PROCEDURE dbo.pr_word_get_by_domain
(
    @account_id int,
    @domain_id int,
    @word_count int,
    @word_start_index int,
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
INNER JOIN domain_word dw
ON w.id = dw.word_id AND dw.domain_id = @domain_id
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @learn_language_id
WHERE w.id NOT IN
    (SELECT word_id FROM account_word
     WHERE account_id = @account_id AND
     language_id = @learn_language_id)
ORDER BY wl.text

SELECT ww.word_id AS id, ww.text AS original, www.text AS [translation]
FROM @word w
INNER JOIN word_language ww
ON w.id = ww.word_id AND ww.language_id = @learn_language_id
INNER JOIN word_language www
ON w.id = www.word_id AND www.language_id = @native_language_id
AND w.number >= @word_start_index
AND w.number <  @word_start_index + @word_count
ORDER BY w.number


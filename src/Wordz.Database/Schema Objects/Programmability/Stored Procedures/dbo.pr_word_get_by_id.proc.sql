
CREATE PROCEDURE dbo.pr_word_get_by_id
(
    @id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT ww.[text] AS original, www.[text] AS [translation]
FROM word_language ww
INNER JOIN word_language www
ON ww.word_id = www.word_id
AND ww.word_id = @id
AND ww.language_id = @learn_language_id
AND www.language_id = @native_language_id


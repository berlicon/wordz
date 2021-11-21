
CREATE PROCEDURE pr_account_word_get_list
(
    @account_id int,
    @language_id int
)
AS

SELECT w.id, wl.text AS original
FROM word w
INNER JOIN account_word aw
ON aw.word_id = w.id
INNER JOIN word_language wl
ON wl.word_id = w.id
WHERE aw.account_id = @account_id
AND aw.language_id = @language_id
AND wl.language_id = @language_id
ORDER BY wl.text


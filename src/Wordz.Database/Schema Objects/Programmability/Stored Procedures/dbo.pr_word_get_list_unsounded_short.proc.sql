
CREATE PROCEDURE dbo.pr_word_get_list_unsounded_short
(
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT DISTINCT w.id--, wl.text, wwl.text
FROM word w
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @native_language_id
INNER JOIN word_language wwl
ON w.id = wwl.word_id AND wwl.language_id = @learn_language_id
WHERE w.[id] > 18656
AND LEN(wl.text) < 21
AND CHARINDEX('''', wl.text) = 0
AND CHARINDEX('-', wl.text) = 0
AND CHARINDEX(' ', wl.text) = 0
AND CHARINDEX('.', wl.text) = 0
AND LEN(wwl.text) < 21
AND CHARINDEX('''', wwl.text) = 0
AND CHARINDEX('-', wwl.text) = 0
AND CHARINDEX(' ', wwl.text) = 0
AND CHARINDEX('.', wwl.text) = 0
ORDER BY w.[id]


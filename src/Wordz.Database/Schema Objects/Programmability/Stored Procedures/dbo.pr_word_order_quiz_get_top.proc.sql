
CREATE PROCEDURE pr_word_order_quiz_get_top
(
    @language_id int
)
AS

SELECT TOP 100
ISNULL(a.nick, q.nick) as nick,
q.result as result
FROM word_order_quiz q
LEFT JOIN account a
ON a.[id] = q.account_id
WHERE q.language_id = @language_id
ORDER BY result DESC, q.id DESC



CREATE PROCEDURE pr_account_word_get_top_20
AS

SELECT TOP 20 a.nick as nick, COUNT(word_id) as [count]
FROM account_word aw
INNER JOIN account a
ON a.[id] = aw.account_id
GROUP BY a.nick
ORDER BY COUNT(word_id) DESC


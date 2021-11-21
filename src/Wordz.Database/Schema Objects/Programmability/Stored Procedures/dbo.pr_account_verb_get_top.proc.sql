
CREATE PROCEDURE pr_account_verb_get_top
AS

SELECT TOP 20 a.nick as nick, COUNT(verb_id) as [count]
FROM account_verb av
INNER JOIN account a
ON a.[id] = av.account_id
GROUP BY a.nick
ORDER BY COUNT(verb_id) DESC


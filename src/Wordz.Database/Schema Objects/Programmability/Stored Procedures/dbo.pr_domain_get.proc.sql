
CREATE PROCEDURE dbo.pr_domain_get
(
    @language_id int
)
AS

SELECT d.[id], (dwl.[name] + ' (' + CAST(COUNT(dw.word_id) AS VARCHAR(10)) + ')') AS [name]
FROM domain d
INNER JOIN domain_word_language dwl
ON dwl.domain_id = d.id AND dwl.language_id = @language_id
LEFT JOIN domain_word dw 
ON d.[id] = dw.domain_id
GROUP BY dw.domain_id, d.[id], dwl.[name]
ORDER BY d.[id]


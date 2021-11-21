
CREATE PROCEDURE dbo.pr_category_get
(
    @language_id int
)
AS

SELECT category_id AS id, [name]
FROM article_category_language
WHERE language_id = @language_id
ORDER BY category_id


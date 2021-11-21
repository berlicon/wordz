
CREATE PROCEDURE dbo.pr_article_get
(
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT id, title, category_id
FROM article
WHERE native_language_id = @native_language_id
AND learn_language_id = @learn_language_id
ORDER BY [order]



CREATE PROCEDURE dbo.pr_article_get_by_id
(
    @id int
)
AS

SELECT a.title, a.body, s.url as site_url
FROM article a
inner join site s
on a.site_id = s.id
where a.id = @id


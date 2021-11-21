
CREATE PROCEDURE dbo.pr_tv_get
(
    @id int,
    @native_language_id int
)
AS

SELECT t.account_id, t.image_url, t.is_editable, t.language_id, t.url, tl.[description], tl.name
FROM  tv t
INNER JOIN tv_language tl
ON tl.tv_id = t.id AND tl.language_id = @native_language_id
WHERE t.id = @id


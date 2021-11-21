
CREATE PROCEDURE dbo.pr_fm_get
(
    @id int,
    @native_language_id int
)
AS

SELECT f.account_id, f.image_url, f.is_editable, f.language_id, f.url, f.use_media_player, fl.[description], fl.name
FROM  fm f
INNER JOIN fm_language fl
ON fl.fm_id = f.id AND fl.language_id = @native_language_id
WHERE f.id = @id


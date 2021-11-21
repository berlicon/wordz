
CREATE PROCEDURE dbo.pr_film_get
(
    @id int,
	@native_language_id int
)
AS

SELECT 
	f.id
	, f.name AS [name]
	, f.[description]
	, f.url AS url
	, p.pattern AS pattern
	, f.film_category_id
	, f.account_id
	, f.player_code
	,f.image_url
FROM  film f
LEFT JOIN film_player p
ON f.film_player_id = p.id
WHERE f.id = @id

SELECT url
FROM  film_part
WHERE film_id = @id
ORDER BY number


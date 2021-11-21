
CREATE PROCEDURE dbo.pr_film_category_get_list
(
    @language_id int
)
AS

SELECT film_category_id AS id, [name]
FROM [film_category_language]
WHERE language_id = @language_id
ORDER BY [name]



CREATE PROCEDURE dbo.pr_film_clear_all
AS

DELETE FROM film_part
DELETE FROM film
DELETE FROM film_player
DELETE FROM film_category_language
DELETE FROM film_category


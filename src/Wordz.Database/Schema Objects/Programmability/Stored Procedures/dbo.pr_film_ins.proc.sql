
CREATE PROCEDURE dbo.pr_film_ins
(
    @name nvarchar(100),
    @category nvarchar(30),
    @player_pattern nvarchar(2000),
    @url nvarchar(100),
    @id int,
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @category_id int
DECLARE @player_id int

SET @category_id = (SELECT film_category_id
                    FROM film_category_language
                    WHERE [name] = @category AND
                    language_id = @native_language_id)
IF (@category_id IS NULL)
    BEGIN
        INSERT INTO film_category DEFAULT VALUES
        SET @category_id = SCOPE_IDENTITY()
        INSERT INTO film_category_language
        (language_id, film_category_id, [name])
        VALUES (@native_language_id, @category_id, @category)
    END

SET @player_id = (SELECT id FROM film_player WHERE pattern = @player_pattern)
IF (@player_id IS NULL)
    BEGIN
        INSERT INTO film_player (pattern) VALUES (@player_pattern)
        SET @player_id = SCOPE_IDENTITY()
    END

INSERT INTO film
([id], [name], [url], [film_category_id], [film_player_id], language_id)
VALUES (@id, @name, @url, @category_id, @player_id, @learn_language_id)


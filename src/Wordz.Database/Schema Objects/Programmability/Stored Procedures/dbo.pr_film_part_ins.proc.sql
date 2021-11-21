
CREATE PROCEDURE dbo.pr_film_part_ins
(
    @url nvarchar(100),
    @id int,
    @number int
)
AS

INSERT INTO film_part
(film_id, number, url)
VALUES (@id, @number, @url)


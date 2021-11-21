CREATE PROCEDURE dbo.pr_film_upd_status
(
    @id int,
    @status bit
)
AS
UPDATE film SET status = @status WHERE id = @id


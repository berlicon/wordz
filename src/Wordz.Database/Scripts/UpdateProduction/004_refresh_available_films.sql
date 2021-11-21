ALTER TABLE film ADD status bit null

UPDATE film SET status = 1 WHERE id = 502

UPDATE film SET status = 1 WHERE id in (
431	,
498	,
499	,
500	,
501	,
502	,
503	,
504	,
508	)

/*select id from film where status is null
select id from film where status = 1
select top 100 id, name, status from film
UPDATE film SET status = 0 WHERE status is null*/

---
CREATE PROCEDURE dbo.pr_film_upd_status
(
    @id int,
    @status bit
)
AS
UPDATE film SET status = @status WHERE id = @id

GRANT  EXECUTE  ON [dbo].[pr_film_upd_status]  TO [wordz_users]

---
ALTER  PROCEDURE dbo.pr_film_get_list_by_category
(
    @category_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT f.id, f.name AS [name], fcl.name AS category
FROM  film f
INNER JOIN film_category c
ON f.film_category_id = c.id
INNER JOIN film_category_language fcl
ON fcl.film_category_id = c.id AND fcl.language_id = @native_language_id
WHERE f.status = 1 AND (@category_id = 0 OR c.id = @category_id) AND f.language_id = @learn_language_id
ORDER BY f.name

---
ALTER  PROCEDURE dbo.pr_film_get_list_by_search
(
    @search nvarchar(10),
    @native_language_id int,
    @learn_language_id int
)
AS

IF (LEN(@search) <= 10)
    BEGIN
        SELECT f.id, f.name AS film, fcl.name AS category
        FROM  film f
        INNER JOIN film_category c
        ON f.film_category_id = c.id
        INNER JOIN film_category_language fcl
        ON fcl.film_category_id = c.id AND fcl.language_id = @native_language_id
        WHERE f.status = 1 AND f.name LIKE '%' + @search + '%' AND f.language_id = @learn_language_id
        ORDER BY f.name
    END


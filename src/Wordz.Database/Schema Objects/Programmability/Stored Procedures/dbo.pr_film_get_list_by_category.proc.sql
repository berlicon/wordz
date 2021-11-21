CREATE  PROCEDURE dbo.pr_film_get_list_by_category
(
	@account_id int,
    @category_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

--SELECT f.id, f.name AS [name], fcl.name AS category
--FROM  film f
--INNER JOIN film_category c
--ON f.film_category_id = c.id
--INNER JOIN film_category_language fcl
--ON fcl.film_category_id = c.id AND fcl.language_id = @native_language_id
--WHERE f.status = 1 AND (@category_id = 0 OR c.id = @category_id) AND f.language_id = @learn_language_id
--ORDER BY f.name


SELECT t.id
	, t.name
	, fct.name 'category'
	, t.image_url
	, t.account_id
FROM  film t
--INNER JOIN film_language tl
--ON tl.film_id = t.id AND tl.language_id = @native_language_id
left join dbo.film_order ord
on ord.film_id = t.id and ord.account_id = @account_id
left join dbo.film_category_language fct
on fct.film_category_id = t.film_category_id
	and fct.language_id = @native_language_id
left join dbo.film_player fp
on fp.id = t.film_player_id

WHERE
	(
		ord.order_in_list is null
		or ord.order_in_list >= 0
	)
	and
	(
		(
			t.language_id = @learn_language_id
			and t.account_id is null
		)
		or 
		(
			t.account_id = @account_id
			and ord.order_in_list >= 0
		)
		or 
		(
			t.language_id = @learn_language_id
			and t.account_id <> @account_id
			and ord.order_in_list >= 0
		)
	)
	and t.status = 1 AND (@category_id = 0 OR t.film_category_id = @category_id) AND t.language_id = @learn_language_id
ORDER BY isnull(ord.order_in_list, 0)
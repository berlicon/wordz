
CREATE PROCEDURE dbo.pr_film_get_list
(
    @account_id int,
	@native_language_id int,
    @learn_language_id int
)
AS

--SELECT t.id, t.url, t.image_url, tl.name, tl.[description], t.[is_editable], t.account_id, t.use_media_player
--	,ISNULL(ord.order_in_list, 0) 'order_in_list'
--FROM  film t
--INNER JOIN film_language tl
--ON tl.film_id = t.id AND tl.language_id = @native_language_id
--left join dbo.film_order ord
--on ord.film_id = t.id and ord.account_id = @account_id

SELECT t.id
	, t.url
	--, t.image_url
	, t.name
	, t.[description]
	, t.[is_editable]
	, t.account_id
	, t.film_category_id
	, fct.name 'category_name'
	, t.player_code
	, fp.pattern 'player_patter'
	, t.image_url
	, t.player_code
	,ISNULL(ord.order_in_list, 0) 'order_in_list'
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
	and t.status = 1
ORDER BY t.id
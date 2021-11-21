CREATE PROCEDURE [dbo].[pr_film_get_other_list]
(
	@account_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

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
from film t
--INNER JOIN fm_language tl
--ON tl.fm_id = t.id AND tl.language_id = @native_language_id
left join dbo.film_category_language fct
on fct.film_category_id = t.film_category_id
	and fct.language_id = @native_language_id
left join dbo.film_player fp
on fp.id = t.film_player_id

WHERE t.is_editable = 0
	and 
	(
		t.language_id = @learn_language_id
		and t.account_id is not null
		and t.account_id <> @account_id
		and 
		(
			not exists (
						select top 1 film_id 
						from film_order 
						where 
							order_in_list >= 0 
							and film_order.account_id = @account_id
							and t.id = film_order.film_id
						)
		)
		or
		(
			t.account_id = @account_id
			and 
			(
				not exists (
							select top 1 film_id 
							from film_order 
							where 
								order_in_list >= 0 
								and film_order.account_id = @account_id
								and t.id = film_order.film_id
							)
			)
		)
		or
		(
			t.language_id = @learn_language_id
			and t.account_id is null
			and exists (
						select top 1 film_id 
						from film_order 
						where 
							order_in_list < 0 
							and film_order.account_id = @account_id
							and t.id = film_order.film_id
						)
		)
	)
	and t.status = 1
ORDER BY t.id


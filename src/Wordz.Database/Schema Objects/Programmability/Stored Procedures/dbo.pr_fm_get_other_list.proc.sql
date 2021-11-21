CREATE PROCEDURE [dbo].[pr_fm_get_other_list]
(
	@account_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT t.id, t.url, t.account_id, t.image_url, tl.name, tl.description, t.[is_editable], t.use_media_player
FROM  fm t
INNER JOIN fm_language tl
ON tl.fm_id = t.id AND tl.language_id = @native_language_id
WHERE t.is_editable = 0
	and 
	(
		t.language_id = @learn_language_id
		and t.account_id is not null
		and t.account_id <> @account_id
		and 
		(
			not exists (
						select top 1 fm_id 
						from fm_order 
						where 
							order_in_list >= 0 
							and fm_order.account_id = @account_id
							and t.id = fm_order.fm_id
						)
		)
		or
		(
			t.account_id = @account_id
			and 
			(
				not exists (
							select top 1 fm_id 
							from fm_order 
							where 
								order_in_list >= 0 
								and fm_order.account_id = @account_id
								and t.id = fm_order.fm_id
							)
			)
		)
		or
		(
			t.language_id = @learn_language_id
			and t.account_id is null
			and exists (
						select top 1 fm_id 
						from fm_order 
						where 
							order_in_list < 0 
							and fm_order.account_id = @account_id
							and t.id = fm_order.fm_id
						)
		)
	)
ORDER BY t.id


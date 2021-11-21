CREATE PROCEDURE [dbo].[pr_tv_get_other_list]
(
	@account_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT t.id, t.url, t.account_id, t.image_url, tl.name, tl.description, t.[is_editable]	
FROM  tv t
INNER JOIN tv_language tl
ON tl.tv_id = t.id AND tl.language_id = @native_language_id
WHERE t.is_editable = 0
	and 
	(
		t.language_id = @learn_language_id
		and t.account_id is not null
		and t.account_id <> @account_id
		and 
		(
			not exists (
						select top 1 tv_id 
						from tv_order 
						where 
							order_in_list >= 0 
							and tv_order.account_id = @account_id
							and t.id = tv_order.tv_id
						)
		)
		or
		(
			t.account_id = @account_id
			and 
			(
				not exists (
							select top 1 tv_id 
							from tv_order 
							where 
								order_in_list >= 0 
								and tv_order.account_id = @account_id
								and t.id = tv_order.tv_id
							)
			)
		)
		or
		(
			t.language_id = @learn_language_id
			and t.account_id is null
			and exists (
						select top 1 tv_id 
						from tv_order 
						where 
							order_in_list < 0 
							and tv_order.account_id = @account_id
							and t.id = tv_order.tv_id
						)
		)
	)
ORDER BY t.id


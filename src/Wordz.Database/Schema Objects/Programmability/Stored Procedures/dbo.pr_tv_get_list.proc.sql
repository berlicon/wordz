
CREATE PROCEDURE dbo.pr_tv_get_list
(
	@account_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT t.id, t.url, t.image_url, tl.name, tl.[description], t.[is_editable], t.account_id
	,ISNULL(ord.order_in_list, 0) 'order_in_list'
FROM  tv t
INNER JOIN tv_language tl
ON tl.tv_id = t.id AND tl.language_id = @native_language_id
left join dbo.tv_order ord
on ord.tv_id = t.id and ord.account_id = @account_id
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
ORDER BY t.id


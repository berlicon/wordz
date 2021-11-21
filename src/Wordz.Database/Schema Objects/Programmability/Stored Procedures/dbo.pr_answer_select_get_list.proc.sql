CREATE PROCEDURE [dbo].[pr_answer_select_get_list]
	@account_id int,
	@exercise_id int
AS
	SELECT ISNULL(a.[parent_id], a.[id]) 'id'
		, a.[exercise_id]
		, a.[text]
		, a.[picture_id]
		, a.[is_right]
	FROM answer a
	WHERE a.[exercise_id] = @exercise_id
		--and
		--	(
		--		dbo.fn_is_show_unapproved_exercise(@account_id, 4, e.id) = 0
		--		and e.parent_id is null
		--		or
		--		(
		--			dbo.fn_is_show_unapproved_exercise(@account_id, 4, e.id) = 1
		--			and not exists (
		--					select innerEx.id
		--					from dbo.exercise_select_text innerEx
		--					where innerEx.parent_id = e.id
		--				)
		--		)
		--	)
CREATE PROCEDURE [dbo].[pr_exercise_select_answer_list]
	@account_id int,
	@exercise_id int
AS
	SELECT ISNULL(a.[parent_id], a.[id]) 'id'
		, a.[exercise_id]
		, a.[text]
		, a.[picture_id]
		, a.[is_right]
	FROM answer a
	WHERE 
		a.[exercise_id] = @exercise_id
		and
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 2, a.[exercise_id]) = 0
				and a.parent_id is null
				or
				(
					dbo.fn_is_show_unapproved_exercise(@account_id, 2, a.[exercise_id]) = 1
					and not exists (
							select innerAns.id
							from dbo.answer innerAns
							where innerAns.parent_id = a.id
						)
				)
			)
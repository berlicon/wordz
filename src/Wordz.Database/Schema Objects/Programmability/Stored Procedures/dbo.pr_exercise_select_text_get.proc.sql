CREATE PROCEDURE [dbo].[pr_exercise_select_text_get]
	@account_id int,
	@exercise_id int
AS
	SELECT @exercise_id 'id'
		, e.[name]
		, e.[description]
		, e.[module_id]
		, e.[text]
		, e.[ordinal_number]
		, e.[is_approved]
		, e.[parent_id]
	FROM exercise_select_text e
	WHERE 
		(
			e.id = @exercise_id
			or e.parent_id = @exercise_id
		)
		and
		(
			dbo.fn_is_show_unapproved_exercise(@account_id, 4, e.id) = 0
			and e.parent_id is null
			or
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 4, e.id) = 1
				and not exists (
						select innerEx.id
						from dbo.exercise_select_text innerEx
						where innerEx.parent_id = e.id
					)
			)
		)
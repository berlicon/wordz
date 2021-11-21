CREATE PROCEDURE [dbo].[pr_exercise_answer_text_get]
	@exercise_id int,
	@account_id int
AS
	declare @is_show_unapproved bit;
	select @is_show_unapproved = dbo.fn_is_show_unapproved_exercise(@account_id, 3, @exercise_id);

	SELECT
		@exercise_id 'id'
		, e.[name]
		, e.[description]
		, e.[module_id]
		, e.[text]
		, e.[ordinal_number]
		, e.[is_approved]
		, e.[parent_id]
	FROM exercise_answer_text e
	WHERE 
		(
			e.[id] = @exercise_id
			or e.parent_id = @exercise_id
		)
		and 
		(
			@is_show_unapproved = 0
			and is_approved = 1
			and parent_id is null
			or
			(
				@is_show_unapproved = 1
				and not exists (
						select innerEx.id 
						from dbo.[exercise_answer_text] innerEx
						where innerEx.parent_id = e.id
					)
			)
		)
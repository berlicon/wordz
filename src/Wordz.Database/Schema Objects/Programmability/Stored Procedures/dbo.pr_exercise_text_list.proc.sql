CREATE PROCEDURE [dbo].[pr_exercise_text_list]
	(@account_id int,
	@module_id int)
AS
	SELECT [id], [name], [description], [module_id], [text], [ordinal_number], [parent_id], [is_approved]
	FROM exercise_text
	WHERE [module_id] = @module_id
CREATE PROCEDURE [dbo].[pr_exercise_text_del]
	(@account_id int,
	 @exercise_text_id int)
AS
DELETE FROM exercise_text
WHERE id = @exercise_text_id
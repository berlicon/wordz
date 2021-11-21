CREATE PROCEDURE [dbo].[pr_exercise_select_del]
	(@account_id int,
	 @exercise_select_id int)
AS
DELETE FROM answer
WHERE exercise_id = @exercise_select_id
DELETE FROM exercise_select
WHERE id = @exercise_select_id
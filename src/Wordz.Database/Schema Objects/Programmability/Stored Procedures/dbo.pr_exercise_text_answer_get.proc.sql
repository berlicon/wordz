CREATE PROCEDURE [dbo].[pr_exercise_text_answer_get]
	@account_id int,
	@exercise_id int
AS
	SELECT [id], [exercise_id], [account_id], [text], [mark]
	FROM exercise_text_answer
	WHERE [exercise_id] = @exercise_id and [account_id] = @account_id
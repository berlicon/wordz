CREATE PROCEDURE [dbo].[pr_exercise_text_answer_ins]
	(@account_id int,
	@exercise_id int,
	@text nvarchar(max))
AS
INSERT INTO exercise_text_answer
([account_id], [exercise_id], [text], [mark])
VALUES (@account_id, @exercise_id, @text, null)

SELECT SCOPE_IDENTITY()
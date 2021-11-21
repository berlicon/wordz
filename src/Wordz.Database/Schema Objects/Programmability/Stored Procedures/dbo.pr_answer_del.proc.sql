CREATE PROCEDURE [dbo].[pr_answer_del]
	(@answer_id int)
AS
DELETE FROM answer
WHERE id = @answer_id
	or parent_id = @answer_id
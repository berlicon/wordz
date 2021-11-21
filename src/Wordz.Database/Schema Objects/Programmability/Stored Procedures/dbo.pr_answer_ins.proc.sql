CREATE PROCEDURE [dbo].[pr_answer_ins]
	(
	@exercise_id int,
	@text nvarchar(max),
	@picture_id int = null,
	@is_right bit
	)
AS
INSERT INTO answer
([exercise_id], [text], [picture_id], [is_right], [is_approved], [parent_id])
VALUES (@exercise_id, @text, @picture_id, @is_right, 0, null)

SELECT SCOPE_IDENTITY()
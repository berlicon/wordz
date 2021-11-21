CREATE PROCEDURE [dbo].[pr_exercise_text_answer_upd]
	(
	@id int,
	@text nvarchar(max),
	@mark int,
	@result_id int out)
AS
if (EXISTS(select id from [dbo].[exercise_text_answer] where id = @id))
begin
	update [dbo].[exercise_text_answer]
	set
		[mark] = @mark,
		[text] = @text
	where id = @id	
	select @result_id = @id
end

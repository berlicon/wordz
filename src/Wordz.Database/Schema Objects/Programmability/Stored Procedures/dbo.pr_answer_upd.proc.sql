CREATE PROCEDURE [dbo].[pr_answer_upd]
	(@answer_id int,
	@exercise_id int,
	@text nvarchar(max),
	@picture_id int = null,
	@is_right bit,
	@result_id int output)
AS
begin
if (EXISTS(select id from [dbo].[answer] where parent_id = @answer_id and is_approved = 0))
begin
	update [dbo].[answer]
	set
		[exercise_id] = @exercise_id,
		[text] = @text,
		[picture_id] = @picture_id,
		[is_right] = @is_right
	where parent_id = @answer_id and is_approved = 0
end
else
begin
	insert into [dbo].[answer]
		([exercise_id],
		[text],
		[picture_id],
		[is_right] ,
		[is_approved],
		parent_id)
	values (
		@exercise_id,
		@text,
		@picture_id,
		@is_right,
		0,
		@answer_id
	);
end
	select @result_id = @answer_id
	end
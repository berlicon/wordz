CREATE PROCEDURE [dbo].[pr_exercise_text_upd]
	(@account_id int,
	@exercise_text_id int,
	@name nvarchar(200),
	@description nvarchar(max),
	@module_id int,
	@text nvarchar(max),
	@ordinal_number int,
	@result_id int output)
AS
begin
if (EXISTS(select id from [dbo].[exercise_text] where parent_id = @exercise_text_id and is_approved = 0))
begin
	update [dbo].[exercise_text]
	set
		[name] = @name,
		[description] = @description,
		[module_id] = @module_id,
		[text] = @text,
		[ordinal_number] = @ordinal_number
	where parent_id = @exercise_text_id and is_approved = 0
end
else
begin
	insert into [dbo].[exercise_text]
		([name],
		[description],
		[module_id],
		[text],
		[ordinal_number],
		[is_approved],
		[parent_id])
	values (
		@name,
		@description,
		@module_id,
		@text,
		@ordinal_number,
		0,
		@exercise_text_id
	);
end
	select @result_id = @exercise_text_id
	end

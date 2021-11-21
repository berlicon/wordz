CREATE PROCEDURE [dbo].[pr_exercise_select_upd]
	(@account_id int,
	@exercise_select_id int,
	@name nvarchar(200),
	@description nvarchar(max),
	@module_id int,
	@text nvarchar(max),
	@picture_id int = null,
	@ordinal_number int, 
	@result_id int output)
AS
begin
if (EXISTS(select id from [dbo].[exercise_select] where parent_id = @exercise_select_id and is_approved = 0))
begin
	update [dbo].[exercise_select]
	set
		[name] = @name,
		[description] = @description,
		[module_id] = @module_id,
		[text] = @text,
		[ordinal_number] = @ordinal_number
	where parent_id = @exercise_select_id and is_approved = 0
end
else
begin
	insert into [dbo].[exercise_select]
		([name],
		[description],
		[module_id],
		[text],
		[picture_id],
		[ordinal_number],
		[is_approved],
		[parent_id])
	values (
		@name,
		@description,
		@module_id,
		@text,
		@picture_id,
		@ordinal_number,
		0,
		@exercise_select_id
	);
end
select @result_id = @exercise_select_id;
end
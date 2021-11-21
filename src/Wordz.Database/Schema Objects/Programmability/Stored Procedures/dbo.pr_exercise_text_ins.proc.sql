CREATE PROCEDURE [dbo].[pr_exercise_text_ins]
	(@account_id int,
	@name nvarchar(200),
	@description nvarchar(max),
	@module_id int,
	@text nvarchar(max))
AS
BEGIN
DECLARE @ordinal_number int;
DECLARE @id int;
SET @ordinal_number=(SELECT exercise_max_number FROM module WHERE id = @module_id) + 1;

UPDATE module SET exercise_max_number = @ordinal_number WHERE id=@module_id;

INSERT INTO exercise_text
([name], [description], [module_id], [text], [ordinal_number], [parent_id], [is_approved])
VALUES (@name, @description, @module_id, @text, @ordinal_number, null, 0)

SET @id = SCOPE_IDENTITY();

SELECT @id as 'id', @ordinal_number as 'ordinal_number';
END
CREATE FUNCTION [dbo].[fn_is_show_unapproved_exercise]
(
	@account_id int,
	@exercise_type int,
	@exercise_id int
)
RETURNS bit
AS
BEGIN
	declare @retVal bit;
	
	select @retVal = ISNULL(is_admin, 0)
	from dbo.account
	where id = @account_id

	if (@retVal is null)
	begin
		select @retVal = 0
	end

	if (@retVal = 0)
	begin
		if @exercise_type = 1
			select @retVal = convert(bit, (select COUNT(*)
										from dbo.exercise_text e
										left join dbo.module m
										on m.id = e.module_id
										left join dbo.course c
										on m.course_id = c.id
										where e.id = @exercise_id
											and c.owner_id = @account_id))
		if @exercise_type = 2
			select @retVal = convert(bit, (select COUNT(*)
										from dbo.exercise_select e
										left join dbo.module m
										on m.id = e.module_id
										left join dbo.course c
										on m.course_id = c.id
										where e.id = @exercise_id
											and c.owner_id = @account_id))
		if @exercise_type = 3
			select @retVal = convert(bit, (select COUNT(*)
										from dbo.exercise_answer_text e
										left join dbo.module m
										on m.id = e.module_id
										left join dbo.course c
										on m.course_id = c.id
										where e.id = @exercise_id
											and c.owner_id = @account_id))
		if @exercise_type = 4
			select @retVal = convert(bit, (select COUNT(*)
										from dbo.exercise_select_text e
										left join dbo.module m
										on m.id = e.module_id
										left join dbo.course c
										on m.course_id = c.id
										where e.id = @exercise_id
											and c.owner_id = @account_id))
		if @exercise_type = 5
			select @retVal = convert(bit, (select COUNT(*)
										from dbo.exercise_skip_text e
										left join dbo.module m
										on m.id = e.module_id
										left join dbo.course c
										on m.course_id = c.id
										where e.id = @exercise_id
											and c.owner_id = @account_id))
	end

	return @retVal;
END
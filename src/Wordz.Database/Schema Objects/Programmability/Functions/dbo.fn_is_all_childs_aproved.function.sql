CREATE FUNCTION [dbo].[fn_is_all_childs_approved]
(
	@course_id int
)
RETURNS bit
AS
BEGIN
	declare @non_approved_count int;
	
	select @non_approved_count = COUNT(*)
	from dbo.module m
	where m.course_id = @course_id
		and m.is_approved = 0
	
	if (@non_approved_count > 0)
		return convert(bit, 0);

	select @non_approved_count = COUNT(*)
	from dbo.exercise_answer_text ex
	left join module m
	on m.id = ex.module_id
	where m.course_id = @course_id
		and ex.is_approved = 0

	if (@non_approved_count > 0)
		return convert(bit, 0);


	select @non_approved_count = COUNT(*)
	from dbo.exercise_select ex
	left join module m
	on m.id = ex.module_id
	where m.course_id = @course_id
		and ex.is_approved = 0

	if (@non_approved_count > 0)
		return convert(bit, 0);

	select @non_approved_count = COUNT(*)
	from dbo.exercise_select_text ex
	left join module m
	on m.id = ex.module_id
	where m.course_id = @course_id
		and ex.is_approved = 0

	if (@non_approved_count > 0)
		return convert(bit, 0);

	select @non_approved_count = COUNT(*)
	from dbo.exercise_skip_text ex
	left join module m
	on m.id = ex.module_id
	where m.course_id = @course_id
		and ex.is_approved = 0

	if (@non_approved_count > 0)
		return convert(bit, 0);

	select @non_approved_count = COUNT(*)
	from dbo.exercise_text ex
	left join module m
	on m.id = ex.module_id
	where m.course_id = @course_id
		and ex.is_approved = 0

	if (@non_approved_count > 0)
		return convert(bit, 0);


	select @non_approved_count = COUNT(*)
	from dbo.answer ans
	left join dbo.exercise_select ex
	on ex.id = ans.exercise_id
	left join module m
	on m.id = ex.module_id
	where m.course_id = @course_id
		and ans.is_approved = 0

	if (@non_approved_count > 0)
		return convert(bit, 0);

	-- Если все Ок, то возвращаем 1
	return convert(bit, 1);
END
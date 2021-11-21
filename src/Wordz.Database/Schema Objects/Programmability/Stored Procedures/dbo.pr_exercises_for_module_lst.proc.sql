CREATE PROCEDURE [dbo].[pr_exercises_for_module_lst]
	@account_id int, 
	@module_id int
AS
	select *
	from
	(
		select ISNULL(e.parent_id, e.id) 'id'
			,e.name 'name'
			,e.[description] 'description'
			,e.[ordinal_number] 'ordinal_number'
			,1 'Type'
		from dbo.exercise_text e
		where
			e.module_id = @module_id
			and 
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 1, e.id) = 0
				and e.parent_id is null
				or
				(
					dbo.fn_is_show_unapproved_exercise(@account_id, 1, e.id) = 1
					and not exists (
							select innerEx.id
							from dbo.exercise_text innerEx
							where innerEx.parent_id = e.id
						)
				)
			)

		union 

		select ISNULL(e.parent_id, e.id) 'id'
			,e.name 'name'
			,e.[description] 'description'
			,e.[ordinal_number] 'ordinal_number'
			,2 'Type'
		from dbo.exercise_select e
		where
			e.module_id = @module_id
			and 
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 2, e.id) = 0
				and e.parent_id is null
				or
				(
					dbo.fn_is_show_unapproved_exercise(@account_id, 2, e.id) = 1
					and not exists (
							select innerEx.id
							from dbo.exercise_select innerEx
							where innerEx.parent_id = e.id
						)
				)
			)

			union 

		select ISNULL(e.parent_id, e.id) 'id'
			,e.name 'name'
			,e.[description] 'description'
			,e.[ordinal_number] 'ordinal_number'
			,3 'Type'
		from dbo.exercise_answer_text e
		where 
			e.module_id = @module_id
			and
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 3, e.id) = 0
				and e.parent_id is null
				or
				(
					dbo.fn_is_show_unapproved_exercise(@account_id, 3, e.id) = 1
					and not exists (
							select innerEx.id
							from dbo.exercise_answer_text innerEx
							where innerEx.parent_id = e.id
						)
				)
			)

			union 

		select ISNULL(e.parent_id, e.id) 'id'
			,e.name 'name'
			,e.[description] 'description'
			,e.[ordinal_number] 'ordinal_number'
			,4 'Type'
		from dbo.exercise_select_text e
		where 
			e.module_id = @module_id
			and
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 4, e.id) = 0
				and e.parent_id is null
				or
				(
					dbo.fn_is_show_unapproved_exercise(@account_id, 4, e.id) = 1
					and not exists (
							select innerEx.id
							from dbo.exercise_select_text innerEx
							where innerEx.parent_id = e.id
						)
				)
			)

			union 

		select ISNULL(e.parent_id, e.id) 'id'
			,e.name 'name'
			,e.[description] 'description'
			,e.[ordinal_number] 'ordinal_number'
			,5 'Type'
		from dbo.exercise_skip_text e
		where 
			e.module_id = @module_id
			and
			(
				dbo.fn_is_show_unapproved_exercise(@account_id, 5, e.id) = 0
				and e.parent_id is null
				or
				(
					dbo.fn_is_show_unapproved_exercise(@account_id, 5, e.id) = 1
					and not exists (
							select innerEx.id
							from dbo.exercise_skip_text innerEx
							where innerEx.parent_id = e.id
						)
				)
			)
	) t1
	order by t1.ordinal_number
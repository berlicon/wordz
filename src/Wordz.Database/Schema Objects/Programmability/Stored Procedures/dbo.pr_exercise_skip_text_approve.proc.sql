CREATE PROCEDURE [dbo].[pr_exercise_skip_text_approve]
	@account_id int, 
	@exercise_id int
AS
	if (dbo.fn_is_exercise_allowed_approve(@account_id, 5, @exercise_id) = 1)
	begin
		begin transaction;
		begin try
			
			update dbo.exercise_skip_text
			set
				dbo.exercise_skip_text.[description] = child.[description]
				,dbo.exercise_skip_text.name = child.name
				--,dbo.exercise_skip_text.ordinal_number = child.ordinal_number
				,dbo.exercise_skip_text.[text] = child.[text]
			from dbo.exercise_skip_text
			inner join dbo.exercise_skip_text child
			on child.parent_id = dbo.exercise_skip_text.id
			where 
				dbo.exercise_skip_text.id = @exercise_id
				and dbo.exercise_skip_text.parent_id is null
				and child.id is not null
			
			delete from dbo.exercise_skip_text 
			where parent_id = @exercise_id
			
			update dbo.exercise_skip_text
			set
				is_approved = 1
			where
				id = @exercise_id

			commit
			select 0;
			return;
		end try
		begin catch
			rollback 
			select -2
			return;
		end catch
	end

	select -1;
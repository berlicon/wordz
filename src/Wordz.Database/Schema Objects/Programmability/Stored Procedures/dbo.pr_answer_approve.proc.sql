CREATE PROCEDURE [dbo].[pr_answer_approve]
	@account_id int, 
	@answer_id int,
	@result_id int output
AS
	declare @exercise_id int;
	select @exercise_id = exercise_id
	from dbo.answer where id = @answer_id
	
	if (dbo.fn_is_exercise_allowed_approve(@account_id, 2, @exercise_id) = 1)
	begin
		begin transaction;
		begin try
			
			update dbo.answer
			set
				dbo.answer.is_right = child.is_right
				, dbo.answer.picture_id = child.picture_id
				, dbo.answer.[text] = child.[text]
			from dbo.answer
			inner join dbo.answer child
			on child.parent_id = dbo.answer.id
			where 
				dbo.answer.id = @answer_id
				and dbo.answer.parent_id is null
				and child.id is not null
			
			delete from dbo.answer 
			where parent_id = @exercise_id
			
			update dbo.answer
			set
				is_approved = 1
			where
				id = @exercise_id

			commit
			select @result_id = 0;
			return;
		end try
		begin catch
			rollback 
			select @result_id = -2
			return;
		end catch
	end

	select @result_id = -1;
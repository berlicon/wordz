CREATE PROCEDURE [dbo].[pr_exercise_select_approve]
	@account_id int, 
	@exercise_id int
AS
	if (dbo.fn_is_exercise_allowed_approve(@account_id, 2, @exercise_id) = 1)
	begin
		begin transaction;
		begin try
			
			update dbo.exercise_select
			set
				dbo.exercise_select.[description] = child.[description]
				,dbo.exercise_select.name = child.name
				--,dbo.exercise_select.ordinal_number = child.ordinal_number
				,dbo.exercise_select.[text] = child.[text]
				,dbo.exercise_select.picture_id = child.picture_id
			from dbo.exercise_select
			inner join dbo.exercise_select child
			on child.parent_id = dbo.exercise_select.id
			where 
				dbo.exercise_select.id = @exercise_id
				and dbo.exercise_select.parent_id is null
				and child.id is not null
			
			delete from dbo.exercise_select 
			where parent_id = @exercise_id
			
			update dbo.exercise_select
			set
				is_approved = 1
			where
				id = @exercise_id

			-- Сохранение ответов 

			declare answer_cursor cursor
			for 
			select id
			from dbo.answer
			where parent_id is null

			declare @answer_id int;

			open answer_cursor;

			fetch next from answer_cursor
			into @answer_id;

			while (@@FETCH_STATUS = 0)
			begin
				declare @anwer_approve_result int;
				exec dbo.pr_answer_approve @account_id, @answer_id, @anwer_approve_result output;

				fetch next from answer_cursor
				into @answer_id;
			end

			close answer_cursor;
			deallocate answer_cursor;

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

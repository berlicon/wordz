-- Аппрув изменения для модуля
CREATE PROCEDURE [dbo].[pr_module_approve]
	@account_id int,
	@module_id int,
	@result_id int = null output
AS
	if (dbo.fn_is_module_allowed_approve(@account_id, @module_id) = 1)
	begin
		begin transaction
		begin try
			update dbo.module
			set
				dbo.module.currency_id = child.currency_id,
				dbo.module.[description] = child.[description],
				dbo.module.[detailed_description] = child.[detailed_description],
				dbo.module.exercise_max_number = child.exercise_max_number,
				dbo.module.links = child.links,
				dbo.module.name = child.name,
				dbo.module.order_in_course = child.order_in_course,
				dbo.module.picture_id = child.picture_id,
				dbo.module.price = child.price,
				dbo.module.tags = child.tags,
				dbo.module.url = child.url,
				dbo.module.is_deleted = child.is_deleted
			from dbo.module
			inner join dbo.module child
			on dbo.module.id = child.parent_id

			delete from dbo.module
			where parent_id = @module_id;

			update dbo.module
			set
				is_approved = 1
			where
				id = @module_id

			declare @exercisesTable table(id int, name nvarchar(max), [description] nvarchar(max), [ordinal_number] int, [Type] int)

			insert into @exercisesTable
			exec dbo.pr_exercises_for_module_lst 
				@account_id = @account_id
				,@module_id = @module_id
			
			declare @ex_approve_result_table table(ret_val int);

			declare ex_cursor cursor
			for
			select [id], [type] 
			from @exercisesTable

			declare @ex_id int;
			declare @ex_type int;

			open ex_cursor;

			fetch next from ex_cursor
			into
			@ex_id, 
			@ex_type

			while (@@FETCH_STATUS = 0)
			begin
				declare @ex_approve_result int;
				set @ex_approve_result = 0;

				if (@ex_type = 1)
				begin
					insert into @ex_approve_result_table
					exec pr_exercise_text_approve @account_id = @account_id, @exercise_id = @ex_id
				end

				if (@ex_type = 2)
				begin
					insert into @ex_approve_result_table
					exec pr_exercise_select_approve @account_id = @account_id, @exercise_id = @ex_id
				end

				if (@ex_type = 3)
				begin
					insert into @ex_approve_result_table
					exec pr_exercise_answer_text_approve @account_id = @account_id, @exercise_id = @ex_id
				end

				if (@ex_type = 4)
				begin
					insert into @ex_approve_result_table
					exec pr_exercise_select_text_approve @account_id = @account_id, @exercise_id = @ex_id
				end

				if (@ex_type = 5)
				begin
					insert into @ex_approve_result_table
					exec pr_exercise_skip_text_approve @account_id = @account_id, @exercise_id = @ex_id
				end

				select @ex_approve_result = ret_val
				from @ex_approve_result_table

				if (@ex_approve_result < 0)
				begin
					raiserror('Cannot approve exercise!', 16, 1);
				end

				delete from @ex_approve_result_table;

				fetch next from ex_cursor
				into
				@ex_id,
				@ex_type
			end

			commit
			select @result_id = 0;
			return;
		end try
		begin catch
			rollback
			select @result_id = -2;
			select ERROR_MESSAGE();
			return;
		end catch

	end

	select @result_id = -1;
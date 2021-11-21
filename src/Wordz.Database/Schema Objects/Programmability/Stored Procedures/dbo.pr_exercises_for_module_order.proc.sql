CREATE PROCEDURE [dbo].[pr_exercises_for_module_order]
	@account_id int,
	@ordering_string varchar(max)
AS
	begin try
		declare @orderingTableTemp table(ex_id int, ex_type int, order_index int);
		insert into @orderingTableTemp
		select 
			convert(int, 
				LEFT(
					LEFT(f.StringValue, charindex('=', f.StringValue) - 1), 
					charindex(',', 
						LEFT(f.StringValue, charindex('=', f.StringValue) - 1)
					) - 1 
				)
			),
			convert(int, 
				RIGHT(
					LEFT(f.StringValue, charindex('=', f.StringValue) - 1), 
					LEN(LEFT(f.StringValue, charindex('=', f.StringValue) - 1))
					 -  charindex(',', 
							LEFT(f.StringValue, charindex('=', f.StringValue) - 1)
						) 
				)
			),
			convert(int, 
				RIGHT(f.StringValue, LEN(f.StringValue) - charindex('=', f.StringValue))
				)
		from dbo.pr_split_string(@ordering_string, ';') f
	
		declare mycursor cursor
		for
		select ex_id, ex_type, order_index
		from @orderingTableTemp

		declare @ex_id int;
		declare @ex_type int
		declare @order_index int;

		open mycursor

		fetch next from mycursor
		into 
		@ex_id,
		@ex_type,
		@order_index

		while (@@FETCH_STATUS = 0)
		begin
			
			if (@ex_type = 1)
			begin
				update exercise_text
				set 
					ordinal_number = @order_index
				where id = @ex_id
			end
			if (@ex_type = 2)
			begin
				update exercise_select
				set 
					ordinal_number = @order_index
				where id = @ex_id
			end
			if (@ex_type = 3)
			begin
				update exercise_answer_text
				set 
					ordinal_number = @order_index
				where id = @ex_id
			end
			if (@ex_type = 4)
			begin
				update exercise_select_text
				set 
					ordinal_number = @order_index
				where id = @ex_id
			end
			if (@ex_type = 5)
			begin
				update exercise_skip_text
				set 
					ordinal_number = @order_index
				where id = @ex_id
			end

			fetch next from mycursor
			into 
			@ex_id,
			@ex_type,
			@order_index
		end
		select 0;
	end try
	begin catch
		select -1;
	end catch

	close mycursor
	deallocate mycursor
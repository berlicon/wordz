CREATE PROCEDURE [dbo].[pr_modules_for_course_order]
	@account_id int,
	@ordering_string varchar(max)
AS
	begin try
		declare @orderingTableTemp table(module_id int, order_index int);
		insert into @orderingTableTemp
		select convert(int, LEFT(f.StringValue, charindex('=', f.StringValue) - 1))
			   , convert(int, RIGHT(f.StringValue, LEN(f.StringValue) - charindex('=', f.StringValue)))
		from dbo.pr_split_string(@ordering_string, ';') f
	
		declare mycursor cursor
		for
		select module_id, order_index
		from @orderingTableTemp

		declare @module_id int;
		declare @order_index int;

		open mycursor

		fetch next from mycursor
		into 
		@module_id,
		@order_index

		while (@@FETCH_STATUS = 0)
		begin
			update dbo.module
			set
				order_in_course = @order_index
			where
				id = @module_id

			fetch next from mycursor
			into 
			@module_id,
			@order_index
		end
		select 0;
	end try
	begin catch
		select -1;
	end catch

	close mycursor
	deallocate mycursor
CREATE PROCEDURE [dbo].[pr_tv_update_order]
	@account_id int,
	@ordering_string varchar(max)
AS
	begin try
		declare @orderingTableTemp table(tv_id int, order_index int);
		insert into @orderingTableTemp
		select convert(int, LEFT(f.StringValue, charindex('=', f.StringValue) - 1))
			   , convert(int, RIGHT(f.StringValue, LEN(f.StringValue) - charindex('=', f.StringValue)))
		from dbo.pr_split_string(@ordering_string, ';') f
	
		declare mycursor cursor
		for
		select tv_id, order_index
		from @orderingTableTemp

		declare @tv_id int;
		declare @order_index int;

		open mycursor

		fetch next from mycursor
		into 
		@tv_id,
		@order_index

		while (@@FETCH_STATUS = 0)
		begin
			update dbo.tv_order
			set
				order_in_list = @order_index
			where
				account_id = @account_id
				and tv_id = @tv_id

			if (@@ROWCOUNT = 0)
				insert into tv_order (account_id, tv_id, order_in_list)
				values (@account_id, @tv_id, @order_index)

			fetch next from mycursor
			into 
			@tv_id,
			@order_index
		end
		select 0;
	end try
	begin catch
		select -1;
	end catch

	close mycursor
	deallocate mycursor
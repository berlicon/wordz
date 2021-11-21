CREATE PROCEDURE [dbo].[pr_fm_update_order]
	@account_id int,
	@ordering_string varchar(max)
AS
	begin try
		declare @orderingTableTemp table(fm_id int, order_index int);
		insert into @orderingTableTemp
		select convert(int, LEFT(f.StringValue, charindex('=', f.StringValue) - 1))
			   , convert(int, RIGHT(f.StringValue, LEN(f.StringValue) - charindex('=', f.StringValue)))
		from dbo.pr_split_string(@ordering_string, ';') f
	
		declare mycursor cursor
		for
		select fm_id, order_index
		from @orderingTableTemp

		declare @fm_id int;
		declare @order_index int;

		open mycursor

		fetch next from mycursor
		into 
		@fm_id,
		@order_index

		while (@@FETCH_STATUS = 0)
		begin
			update dbo.fm_order
			set
				order_in_list = @order_index
			where
				account_id = @account_id
				and fm_id = @fm_id

			if (@@ROWCOUNT = 0)
				insert into fm_order (account_id, fm_id, order_in_list)
				values (@account_id, @fm_id, @order_index)

			fetch next from mycursor
			into 
			@fm_id,
			@order_index
		end
		select 0;
	end try
	begin catch
		select -1;
	end catch

	close mycursor
	deallocate mycursor
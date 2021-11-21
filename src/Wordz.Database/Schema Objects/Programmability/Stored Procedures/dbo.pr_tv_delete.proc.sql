CREATE PROCEDURE [dbo].[pr_tv_delete]
	@account_id int,
	@tv_id int
AS
	begin try
		declare @owner int = null;
		select @owner = tv.account_id from tv
		where tv.id = @tv_id 

		if (@owner is not null)
		begin
			delete from tv_order
			where tv_order.account_id = @account_id
				and tv_order.tv_id = @tv_id

			if (0 = (select count(*) from dbo.tv_order where tv_id = @tv_id))
			begin
				begin try
					delete from tv
					where id = @tv_id
				end try
				begin catch
				end catch
			end
		end
		else
		begin
			update tv_order 
			set 
				order_in_list = -1
			where
				account_id = @account_id
				and tv_id = @tv_id

			if (@@ROWCOUNT = 0)
				insert into tv_order (account_id, tv_id, order_in_list)
				values (@account_id, @tv_id, -1);
		end
	end try
	begin catch
		select -1;
		return;
	end catch

	select 0;
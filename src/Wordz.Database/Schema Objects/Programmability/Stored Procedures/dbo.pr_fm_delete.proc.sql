CREATE PROCEDURE [dbo].[pr_fm_delete]
	@account_id int,
	@fm_id int
AS
	begin try
		declare @owner int = null;
		select @owner = fm.account_id from fm
		where fm.id = @fm_id 

		if (@owner is not null)
		begin
			delete from fm_order
			where fm_order.account_id = @account_id
				and fm_order.fm_id = @fm_id

			if (0 = (select count(*) from dbo.fm_order where fm_id = @fm_id))
			begin
				begin try
					delete from fm
					where id = @fm_id
				end try
				begin catch
				end catch
			end
		end
		else
		begin
			update fm_order 
			set 
				order_in_list = -1
			where
				account_id = @account_id
				and fm_id = @fm_id

			if (@@ROWCOUNT = 0)
				insert into fm_order (account_id, fm_id, order_in_list)
				values (@account_id, @fm_id, -1);
		end
	end try
	begin catch
		select -1;
		return;
	end catch

	select 0;
CREATE PROCEDURE [dbo].[pr_film_delete]
	@account_id int,
	@film_id int
AS
	begin try
		declare @owner int = null;
		select @owner = film.account_id from film
		where film.id = @film_id 

		if (@owner is not null)
		begin
			delete from film_order
			where film_order.account_id = @account_id
				and film_order.film_id = @film_id

			if (0 = (select count(*) from dbo.film_order where film_id = @film_id))
			begin
				begin try
					delete from film
					where id = @film_id
				end try
				begin catch
				end catch
			end
		end
		else
		begin
			update film_order 
			set 
				order_in_list = -1
			where
				account_id = @account_id
				and film_id = @film_id

			if (@@ROWCOUNT = 0)
				insert into film_order (account_id, film_id, order_in_list)
				values (@account_id, @film_id, -1);
		end
	end try
	begin catch
		select -1;
		return;
	end catch

	select 0;
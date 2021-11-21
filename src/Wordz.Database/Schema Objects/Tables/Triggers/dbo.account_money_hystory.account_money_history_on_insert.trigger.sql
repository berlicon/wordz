CREATE TRIGGER [account_money_history_on_insert]
    ON [dbo].[account_money_hystory]
    AFTER INSERT
    AS 
    BEGIN
		declare @account_id int;
		declare @value decimal(18, 2);
		declare @currency_id int;
		declare @payment_date datetime;
		
		declare amh_cursor cursor for 
		select account_id,
			 change_date,
			 income_value,
			 currency_id
		from inserted
		
		open amh_cursor

		fetch next from amh_cursor
		into @account_id,
			@payment_date,
			@value,
			@currency_id

		while (@@FETCH_STATUS = 0)
		begin
		
    		update dbo.account_money_balance
				set value = value + @value,
					last_update = @payment_date
			where account_id = @account_id
				and currency_id = @currency_id

			if (@@ROWCOUNT = 0)
			begin
				insert into dbo.account_money_balance
					(account_id, currency_id, value, last_update)
				values (@account_id, @currency_id, @value, @payment_date)
			end

			fetch next from amh_cursor
			into @account_id,
				@payment_date,
				@value,
				@currency_id
		end

		close amh_cursor
		deallocate amh_cursor
    END

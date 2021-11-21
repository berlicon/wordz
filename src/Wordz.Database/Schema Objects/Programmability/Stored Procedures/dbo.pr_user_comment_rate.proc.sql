
-- задает рейтинг коменту

CREATE PROCEDURE [dbo].[pr_user_comment_rate]
	@account_id int, 
	@user_comment_id int,
	@is_positive bit
AS
	if (not exists(
			select id 
			from user_comment_rates_and_claims urc 
			where urc.account_id = @account_id 
				and urc.user_comment_id = @user_comment_id)
		and not exists(
			select id
			from user_comment u
			where u.account_id = @account_id
				and u.id = @user_comment_id)
		)
	begin
		insert into user_comment_rates_and_claims 
			(account_id, is_claimed, is_positive_rate, is_rated, user_comment_id)
		values(@account_id, 0, @is_positive, 1, @user_comment_id)

		update user_comment
		set
			rating = rating + case when @is_positive = 1 then 1 else -1 end
		where
			id = @user_comment_id
	end

	select u.rating
	from user_comment u
	where id = @user_comment_id
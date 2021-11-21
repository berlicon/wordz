
-- записывает "жалобу" на определнный комент
CREATE PROCEDURE [dbo].[pr_user_comment_claim]
	@account_id int,
	@user_comment_id int
AS
	update user_comment_rates_and_claims
	set
		is_claimed = 1
	where
		account_id = @account_id
		and user_comment_id = @user_comment_id
	
	if (@@ROWCOUNT = 0)
	begin
		insert into user_comment_rates_and_claims 
			(account_id, is_claimed, is_positive_rate, is_rated, user_comment_id)
		values(@account_id, 1, 0, 0, @user_comment_id)
	end

	update user_comment
	set
		claims_count = claims_count + 1
	where id = @user_comment_id

	select 1;
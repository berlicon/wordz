ALTER TABLE [dbo].[user_comment_rates_and_claims]
	ADD CONSTRAINT [FK_user_comment_rates_and_claims_account_id] 
	FOREIGN KEY (account_id)
	REFERENCES dbo.account (id)	


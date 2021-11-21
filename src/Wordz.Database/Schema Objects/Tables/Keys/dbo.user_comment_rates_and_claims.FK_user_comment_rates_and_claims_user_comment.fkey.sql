ALTER TABLE [dbo].[user_comment_rates_and_claims]
	ADD CONSTRAINT [FK_user_comment_rates_and_claims_user_comment] 
	FOREIGN KEY (user_comment_id)
	REFERENCES user_comment (id)	


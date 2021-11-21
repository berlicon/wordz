CREATE PROCEDURE [dbo].[pr_user_comment_items_count]
	@account_id int, 
	@target_element uniqueidentifier
AS
	select count(id) from dbo.user_comment uc
	where uc.target_element = @target_element
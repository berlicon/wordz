-- Change Date: 2013.05.21
-- Description: хранимка добавления комментария.
--   При указании корневого id комментария, на который будет дан ответ, 
--   значение уровня ответа вычисляется как уровень_предыдущего + 1.

CREATE PROCEDURE [dbo].[pr_user_comment_add]
	@account_id int,
	@target_element uniqueidentifier,
	@comment_text nvarchar(500), 
	@created_date datetime,
	@parent_comment_id int = null,
	@result_id int = null output
AS
	declare @answer_level int
	set @answer_level = 0
	if (@parent_comment_id IS NOT NULL)
	begin
		declare @parent_answer_level int;
		select @parent_answer_level = answer_level + 1 from dbo.user_comment where id = @parent_comment_id;
		select @answer_level = case when @parent_answer_level > 4 then 4 else @parent_answer_level end;
	end

	insert into dbo.user_comment 
		(account_id, 
		answer_level, 
		claims_count, 
		comment_text, 
		target_element, 
		created_date, 
		rating)
	values
		(@account_id,
		@answer_level,
		0,
		@comment_text,
		@target_element,
		@created_date,
		0
		);
	select @result_id = SCOPE_IDENTITY()

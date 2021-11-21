-- Change Date: 2013.05.20
-- Description: обновляет рейтинг, выставленный пользователем для курса

CREATE PROCEDURE [dbo].[pr_rating_update]
	@account_id int, 
	@target_element uniqueidentifier,
	@value float
AS
	update dbo.rating
	set
		value = @value
	where 
		target_element = @target_element
		and account_id = @account_id

	if (@@ROWCOUNT = 0)
	begin
		insert into dbo.rating
			(account_id, target_element, value)
		values 
			(@account_id, @target_element, @value)
	end

	declare @countOfItems float;
	declare @sumOfItems float;

	select @countOfItems = count(*),
		   @sumOfItems = SUM(value)
	from dbo.rating r
	where target_element = @target_element
		and account_id is not null;

	update dbo.rating
	set
		value = @sumOfItems / @countOfItems
	where
		account_id is null
		and target_element = @target_element
	
	if @@ROWCOUNT = 0
	begin
		insert into dbo.rating
		(account_id, target_element, value)
		values (null, @target_element, convert(float, @sumOfItems) / @countOfItems)
	end
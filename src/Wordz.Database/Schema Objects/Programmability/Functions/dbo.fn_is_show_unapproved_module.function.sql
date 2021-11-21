CREATE FUNCTION [dbo].[fn_is_show_unapproved_module]
(
	@account_id int, 
	@module_id int
)
RETURNS INT
AS
BEGIN
	declare @retVal bit;
	
	select @retVal = ISNULL(is_admin, 0)
	from dbo.account
	where id = @account_id

	if (@retVal is null)
	begin
		select @retVal = 0
	end
	
	if (@retVal = 0)
	begin
		select @retVal = convert(bit, (select COUNT(*)
										from dbo.module m
										left join dbo.course c
										on m.course_id = c.id
										where m.id = @module_id
											and c.owner_id = @account_id))
	end

	return @retVal;
END
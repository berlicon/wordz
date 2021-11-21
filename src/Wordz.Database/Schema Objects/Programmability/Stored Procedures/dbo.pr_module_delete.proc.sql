CREATE PROCEDURE [dbo].[pr_module_delete]
	@account_id int,
	@module_id int
AS
	DECLARE @RC int
	DECLARE @course_id int
	DECLARE @name nvarchar(100)
	DECLARE @description nvarchar(300)
	DECLARE @detailed_description nvarchar(max)
	DECLARE @picture_id int
	DECLARE @currency_id int
	DECLARE @price decimal(18,2)
	DECLARE @url varchar(100)
	DECLARE @tags nvarchar(1000)
	DECLARE @links nvarchar (4000)
	DECLARE @exercise_max_number int
	DECLARE @order_in_course int

	select
		@course_id = m.course_id
		,@name = m.name
		,@description = m.[description]
		,@detailed_description = m.detailed_description
		,@picture_id = m.picture_id
		,@currency_id = m.currency_id
		,@price = m.price
		,@url = m.url
		,@tags = m.tags
		,@links = m.links
		,@exercise_max_number = m.exercise_max_number
		,@order_in_course = m.order_in_course
	from dbo.module m
	where 
	(
		m.id = @module_id
		or m.parent_id = @module_id
	)
	and
	(
		not exists (
				select innerModule.id 
				from dbo.module innerModule
				where m.id = innerModule.parent_id
			)
	)

	-- сперва надо проапдейтить запись

	execute @RC = [dbo].[pr_module_update]
	   @account_id
	  ,@module_id
	  ,@course_id
	  ,@name
	  ,@description
	  ,@detailed_description
	  ,@picture_id
	  ,@currency_id
	  ,@price
	  ,@url
	  ,@tags
	  ,@links
	  ,@exercise_max_number
	  ,@order_in_course

	update dbo.module
	set
		is_deleted = 1
	where
		parent_id = @module_id
		and is_approved = 0

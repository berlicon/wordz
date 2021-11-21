CREATE PROCEDURE [dbo].[pr_course_delete]
	@account_id int,
	@course_id int
AS
	DECLARE @RC int
	DECLARE @number uniqueidentifier
	DECLARE @name nvarchar(100)
	DECLARE @description nvarchar(300)
	DECLARE @detailed_description nvarchar(max)
	DECLARE @picture_id int
	DECLARE @currency_id int
	DECLARE @price decimal(18,2)
	DECLARE @ui_langauge_id int
	DECLARE @location_id int
	DECLARE @category_id int
	DECLARE @url varchar(100)
	DECLARE @authors nvarchar(1000)
	DECLARE @contacts nvarchar(1000)
	DECLARE @tags nvarchar(1000)
	DECLARE @links nvarchar(4000)
	DECLARE @is_editable bit
	DECLARE @is_copied bit
	DECLARE @is_public bit
	DECLARE @password nvarchar(100)
	DECLARE @google_advertisement nvarchar(max)
	DECLARE @is_approved bit
	DECLARE @result_id int

	select
		@number = c.number
		,@name = c.name
		,@description = c.[description]
		,@detailed_description = c.[detailed_description]
		,@picture_id = c.picture_id
		,@currency_id = c.currency_id
		,@price = c.price
		,@ui_langauge_id = c.ui_langauge_id
		,@location_id = c.location_id
		,@category_id = c.category_id
		,@url = c.url
		,@authors = c.authors
		,@contacts = c.contacts
		,@tags = c.tags
		,@links = c.links
		,@is_editable = c.is_editable
		,@is_copied = c.is_copied 
		,@is_public = c.is_public
		,@password = c.[password]
		,@google_advertisement = c.google_advertisement
		,@is_approved = c.is_approved
	from dbo.course c
	where
	(
		c.[id] = @course_id
		or c.[parent_id] = @course_id
	)
	and 
	(
		not exists (
					select innerCourse.id
					from dbo.course innerCourse
					where innerCourse.parent_id = c.id
				)
	)
	-- TODO: Set parameter values here.

	EXECUTE @RC = [dbo].[pr_course_update] 
	   @account_id
	  ,@course_id
	  ,@number
	  ,@name
	  ,@description
	  ,@detailed_description
	  ,@picture_id
	  ,@currency_id
	  ,@price
	  ,@ui_langauge_id
	  ,@location_id
	  ,@category_id
	  ,@url
	  ,@authors
	  ,@contacts
	  ,@tags
	  ,@links
	  ,@is_editable
	  ,@is_copied
	  ,@is_public
	  ,@password
	  ,@google_advertisement
	  ,@is_approved
	  ,@result_id OUTPUT

	update dbo.course
	set
		is_deleted = 1
	where
		parent_id = @course_id
		and is_approved = 0

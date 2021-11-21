-- Change Date: 2013.05.20
-- Description: хранимая процедура, которая обновляет
--  курс по его id

create procedure [dbo].[pr_course_update]
(
	@account_id int
	,@course_id int
	,@number uniqueidentifier
	,@name nvarchar(100)
	,@description nvarchar(300)
	,@detailed_description ntext
	,@picture_id int = null
	,@currency_id int
	,@price decimal(18,2)
	,@ui_langauge_id int
	,@location_id int = null
	,@category_id int
	,@url varchar(100)
	,@authors nvarchar(1000)
	,@contacts nvarchar(1000)
	,@tags nvarchar(1000)
	,@links nvarchar(4000)
	,@is_editable bit
	,@is_copied bit
	,@is_public bit
	,@password nvarchar(100) = null
	,@google_advertisement nvarchar(max)
	,@is_approved bit
	,@result_id int output
)
as
if (EXISTS(select id from [dbo].[course] where parent_id = @course_id and is_approved = 0))
begin
	update [dbo].[course]
	set
		number = @number 
		,name = @name 
		,[description] = @description 
		,detailed_description = @detailed_description 
		,picture_id = case when @picture_id = -1 then null
							when @picture_id is not null then @picture_id
						else picture_id end
		,currency_id = @currency_id
		,price = @price 
		,ui_langauge_id = @ui_langauge_id 
		,location_id = @location_id 
		,category_id = @category_id 
		,url = @url 
		,authors = @authors 
		,contacts = @contacts 
		,tags = @tags 
		,links = @links 
		,is_editable = @is_editable 
		,is_copied = @is_copied 
		,is_public = @is_public 
		,[password] = @password 
		,google_advertisement = @google_advertisement 
		--,is_approved = @is_approved 
	where parent_id = @course_id
		 and is_approved = 0
	select @result_id = @course_id
end
else
begin
	declare @parent_id int;
	select @parent_id = case 
							when @course_id > 0 then @course_id 
							when @course_id = 0 then null
							else -1 
						end;

	declare @parent_picture_id int;
	declare @parent_number uniqueidentifier;
	
	if (@parent_id is not null)
	begin
		select 
			@parent_picture_id = picture_id
			,@parent_number = number
		from dbo.course
		where id = @course_id
	end
	else
	begin
		set @parent_number = NEWID();
		set @parent_picture_id = null;
	end

	insert into [dbo].[course]
		(number ,name ,[description]
		,detailed_description ,picture_id ,currency_id 
		,price ,ui_langauge_id ,location_id
		,category_id ,url ,authors ,contacts
		,tags ,links ,is_editable ,is_copied
		,is_public ,[password] ,google_advertisement
		,is_approved ,parent_id, owner_id
		)
	values (
		@parent_number 
		,@name 
		,@description 
		,@detailed_description 
		,case when @picture_id > 0 then @picture_id
				else @parent_picture_id end
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
		,0
		,@parent_id
		,@account_id
	)
	select @result_id = case
							when @parent_id is null then SCOPE_IDENTITY()
							else @course_id
						end;
end

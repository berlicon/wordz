CREATE PROCEDURE [dbo].[pr_tv_update_or_insert]
	@account_id int,
    @language_id int,
	@native_language_id int,
	@id int,
	@url nvarchar(max),
	@image_url nvarchar(max), 
	@name nvarchar(max), 
	@description nvarchar(max),
	@is_editable bit
AS
	declare @isEditableField bit;
	declare @accountIdField int;
	declare @existingTvId int;
	select @existingTvId = id, @isEditableField = is_editable, @accountIdField = account_id
	from dbo.tv
	where id = @id

	if @existingTvId is not null
		and 
		(
			@accountIdField <> @account_id
			or ISNULL(@isEditableField, 0) = 0
		)
	begin
		-- Нельзя редактировать данную запись, т.к.
		-- она не помечена на редактирование 
		-- или не пренадлежит текущему пользователю
		select -1;
		return;
	end
	
	update dbo.tv
	set
		--account_id = @account_id
		--,is_editable = @is_editable
		image_url = @image_url
		,url = @url
		,language_id = @language_id
	where 
		id = @id

	if (@@ROWCOUNT = 0)
	begin
		insert into dbo.tv
			(account_id, image_url, is_editable, language_id, url)
		values
			(@account_id, @image_url, 1, @language_id, @url)
		select @id = SCOPE_IDENTITY()

		insert into dbo.tv_order
			(order_in_list, tv_id, account_id)
		values
			--( (
			--	select case when MAX(order_in_list) > 0 then MAX(order_in_list) else 0 end
			--	from dbo.tv_order ord
			--	where ord.account_id = @account_id
			--  )
			--, @id)
			(0, @id, @account_id)
	end

	update dbo.tv_language
	set
		[description] = @description
		,name = @name
	where 
		tv_id = @id
		and language_id = @native_language_id
	if (@@ROWCOUNT = 0)
	begin
		insert into dbo.tv_language
			(tv_id, language_id, [description], name)
		values
			(@id, @native_language_id, @description, @name)
	end

	select @id
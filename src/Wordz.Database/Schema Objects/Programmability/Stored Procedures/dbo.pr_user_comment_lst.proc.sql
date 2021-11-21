-- Change Date: 2013.05.21
-- Description: хранимка получения комментариев
--   параметр @page_number int считается с 1

CREATE PROCEDURE [dbo].[pr_user_comment_lst]
	@account_id int, 
	@target_element uniqueidentifier,
	@page_size int,
	@page_number int --страница считается с 1
AS
	declare @startIndex int;
	set @startIndex = (@page_number - 1) * @page_size + 1;
	declare @endIndex int;
	set @endIndex = @page_number * @page_size;
	
	with commentCTE as
	(
		select uc.account_id
			,uc.answer_level
			,uc.claims_count
			,uc.comment_text
			,uc.target_element
			,uc.created_date
			,uc.id
			,uc.rating
			,row_number() over (order by uc.created_date desc) as rowNum
		from dbo.user_comment uc
		where uc.target_element = @target_element
	)

	select 
		*
		, acc.nick 'user_name' 
		--, (	
		--	select top 1 COUNT(*) 
		--	from user_comment_rates_and_claims 
		--	where user_comment_id = commentCTE.id
		--		and account_id = @account_id
		--  ) ''
	from commentCTE
	left join account acc
	on acc.id = commentCTE.account_id
	where rowNum between @startIndex and @endIndex
	order by commentCTE.created_date desc
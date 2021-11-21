CREATE PROCEDURE [pr_course_get_list]
(
	@account_id int,
	@is_public bit
)
AS

	declare @is_show_unapproved bit;
	
	SELECT case when course.parent_id is null then course.id else course.parent_id end 'id'
		, course.authors
		, course.category_id 
		, course.contacts
		, course.currency_id
		, course.[description]
		, course.detailed_description
		, course.google_advertisement
		, course.is_approved
		, course.is_copied
		, course.is_editable
		, course.is_public
		, course.links
		, course.location_id
		, course.name
		, course.number
		, course.[password]
		, course.picture_id
		, course.price
		, course.tags
		, course.ui_langauge_id
		, course.url
		, course.parent_id 
		, category.name as category_name
		, lang.name as 'ui_langauge_name'
		, course.owner_id
		, cur.name 'currency_name'
	FROM course
	INNER JOIN category ON course.category_id = category.id
	left join [dbo].[language] lang on lang.id = [ui_langauge_id]
	left join [dbo].currency cur on cur.id = course.currency_id
	WHERE is_public = @is_public
	and 
	(
		[dbo].[fn_is_show_unapproved_course](@account_id, course.id) = 0
		and is_approved = 1
		and parent_id is null
		or
		(
			[dbo].[fn_is_show_unapproved_course](@account_id, course.id) = 1
			and not exists (
					select innerCourse.id 
					from dbo.course innerCourse
					where innerCourse.parent_id = course.id
				)
		)
	)
	ORDER BY course.id

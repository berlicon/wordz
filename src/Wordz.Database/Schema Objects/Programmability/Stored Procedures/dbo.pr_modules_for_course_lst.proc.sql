-- Change Date: 2013.05.20
-- Description: хранимая процедура, которая 
--  выводит список модулей для курса

create procedure [dbo].[pr_modules_for_course_lst]
(
	@account_id int
	,@course_id int
)
as

select ISNULL(m.parent_id, m.id) 'id'
      ,m.[number]
      ,m.[course_id]
      ,m.[name]
      ,m.[description]
      ,m.[detailed_description]
      ,m.[picture_id]
	  ,m.[currency_id]
      ,m.[price]
      ,m.[url]
      ,m.[tags]
      ,m.[links]
	  ,m.[is_approved]
      ,m.[order_in_course]
	  ,convert(bit, ( select top 1 
			case when pt.id is not null or m.price = 0 then 1 
				else 0 
			end 
		 from dbo.payment pt 
		 where pt.module_id = ISNULL(m.parent_id, m.id) and pt.account_id = @account_id)) as 'is_payd'
	 ,m.[exercise_max_number]
	 ,cur.name 'currency_name'
	 ,cr.owner_id 'owner_id'
	 ,m.price * ISNULL(dbo.fn_get_currency_rate(m.currency_id, cr.currency_id, GETUTCDATE()), 1.0) 'price_in_course_currency'
	 ,convert(float, (
					select top 1 (rs.value)
					from dbo.rating rs 
					where account_id is null
						and m.number = rs.target_element)
						) 'total_rate'
from [dbo].[module] m
left join dbo.currency cur
on cur.id = m.currency_id
left join dbo.course cr
on cr.id = m.course_id
where [course_id] = @course_id
	and 
	(
		dbo.fn_is_show_unapproved_module(@account_id, m.id) = 0
		and m.is_approved = 1
		and m.parent_id is null
		or
		(
			dbo.fn_is_show_unapproved_module(@account_id, m.id) = 1
			and not exists (
					select innerModule.id 
					from dbo.module innerModule
					where m.id = innerModule.parent_id
				)
		)
	)
	and isnull(m.is_deleted, 0) = 0
order by  m.[order_in_course]
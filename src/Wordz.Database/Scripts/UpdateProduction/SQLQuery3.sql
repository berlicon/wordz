set

select * from dbo.account
select COUNT(*) from dbo.account


select * from dbo.course
select * from dbo.module
--select * from dbo.answer where module_id=1
select * from dbo.exercise_answer_text  where module_id=1
select * from dbo.exercise_select where module_id=1
select * from dbo.exercise_select_text where module_id=1
select * from dbo.exercise_skip_text where module_id=1
select * from dbo.exercise_text where module_id=1
--select * from dbo.exercise_text_answer where module_id=1



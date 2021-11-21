select * from dbo.verb_type

select * from dbo.verb
where verb_type_id = 13

/*update dbo.verb
set 
form1 = LTRIM(RTRIM(form1)),
form2 = LTRIM(RTRIM(form2)),
form3 = LTRIM(RTRIM(form3)),
[translation] = LTRIM(RTRIM([translation]))*/

select * from dbo.verb
where 
form1 <> form2 and
((form3 = form2 + SUBSTRING(REVERSE(form2),1,1) + 'en') or
(form3 = SUBSTRING(form2,1,LEN(form2) - 1) + 'n'))
and verb_type_id = 13

/*
update dbo.verb
set verb_type_id = 12
where
form1 <> form2 and
((form3 = form2 + SUBSTRING(REVERSE(form2),1,1) + 'en') or
(form3 = SUBSTRING(form2,1,LEN(form2) - 1) + 'n'))
and verb_type_id = 13

update dbo.verb
set verb_type_id = 11
where
form1 <> form2 and
form2 = form3
and verb_type_id = 13

update dbo.verb
set verb_type_id = 10
where
((SUBSTRING(REVERSE(form1),1,2) = 'ni' and 
SUBSTRING(REVERSE(form2),1,2) = 'na' and 
SUBSTRING(REVERSE(form3),1,2) = 'nu') or
(SUBSTRING(REVERSE(form1),1,3) = 'kni' and 
SUBSTRING(REVERSE(form2),1,3) = 'kna' and 
SUBSTRING(REVERSE(form3),1,3) = 'knu') or
(SUBSTRING(REVERSE(form1),1,2) = 'mi' and 
SUBSTRING(REVERSE(form2),1,2) = 'ma' and 
SUBSTRING(REVERSE(form3),1,2) = 'mu') or
(SUBSTRING(REVERSE(form1),1,3) = 'gni' and 
SUBSTRING(REVERSE(form2),1,3) = 'gna' and 
SUBSTRING(REVERSE(form3),1,3) = 'gnu'))
and verb_type_id = 13

update dbo.verb
set verb_type_id = 9
where
SUBSTRING(REVERSE(form2),1,2) = 'we'
and form3 = form1 + 'n'
and SUBSTRING(REVERSE(form3),1,3) = 'nwo'

update dbo.verb
set verb_type_id = 8
where
form1 <> form2 and form3 <> form2
and (form3 = form1 + 'en' or form3 = form1 + 'n')
and verb_type_id = 13

update dbo.verb
set verb_type_id = 7
where
(SUBSTRING(REVERSE(form1),1,3) = 'evi' or 
SUBSTRING(REVERSE(form1),1,3) = 'edi' or 
SUBSTRING(REVERSE(form1),1,3) = 'esi') and
(SUBSTRING(REVERSE(form2),1,3) = 'evo' or 
SUBSTRING(REVERSE(form2),1,3) = 'edo' or 
SUBSTRING(REVERSE(form2),1,3) = 'eso') and
(SUBSTRING(REVERSE(form3),1,3) = 'nev' or 
SUBSTRING(REVERSE(form3),1,3) = 'ned' or 
SUBSTRING(REVERSE(form3),1,3) = 'nes')
and verb_type_id = 13

update dbo.verb
set verb_type_id = 6
where
form2 = form3
and form2 = form1 + 't'
and verb_type_id = 13

update dbo.verb
set verb_type_id = 5
where
(SUBSTRING(REVERSE(form2),1,3) = 'eko' or SUBSTRING(REVERSE(form2),1,3) = 'eso')
and (SUBSTRING(REVERSE(form3),1,4) = 'neko' or SUBSTRING(REVERSE(form3),1,4) = 'neso')
and verb_type_id = 13

update dbo.verb
set verb_type_id = 4
where
form2 = form3
and (SUBSTRING(REVERSE(form2),1,4) = 'thgu' or SUBSTRING(REVERSE(form2),1,2) = 'to')
and verb_type_id = 13

update dbo.verb
set verb_type_id = 3
where (CHARINDEX('ee', form1) > 0 or CHARINDEX('ea', form1) > 0)
and form2 = form3 and SUBSTRING(REVERSE(form2),1,1) = 't'
and verb_type_id = 13

update dbo.verb
set verb_type_id = 2
where SUBSTRING(REVERSE(form1),1,1) = 'd'
and form2 = form3 and SUBSTRING(REVERSE(form2),1,1) = 't'
and verb_type_id = 13

update dbo.verb
set verb_type_id = 1
where form1 = form2 and form2 = form3
and verb_type_id = 13
*/





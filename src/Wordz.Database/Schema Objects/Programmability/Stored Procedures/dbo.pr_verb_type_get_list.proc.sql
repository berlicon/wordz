
CREATE PROCEDURE pr_verb_type_get_list
AS

SELECT id, [rule]
FROM verb_type
ORDER BY id


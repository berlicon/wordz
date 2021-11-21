
CREATE PROCEDURE pr_language_get_list
AS

SELECT id, [name]
FROM [language]
ORDER BY [name]


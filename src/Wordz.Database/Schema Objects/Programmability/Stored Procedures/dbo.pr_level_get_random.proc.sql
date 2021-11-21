CREATE PROCEDURE pr_level_get_random
(
    @language_id int
)
AS

SELECT TOP 1 sentence, answer1, answer2, answer3, correct
FROM level
WHERE language_id = @language_id
ORDER BY NEWID()

CREATE PROCEDURE pr_word_order_get_random
(
    @language_id int
)
AS

SELECT TOP 1 sentence
FROM word_order
WHERE language_id = @language_id
ORDER BY NEWID()



CREATE PROCEDURE pr_account_word_get_count
(
    @account_id int,
    @language_id int
)
AS

SELECT COUNT(*) AS [count] FROM account_word
WHERE account_id = @account_id AND language_id = @language_id



CREATE PROCEDURE pr_account_word_get
(
    @account_id int,
    @word_id int,
    @language_id int
)
AS

SELECT account_id FROM account_word
WHERE account_id = @account_id
AND word_id = @word_id
AND language_id = @language_id


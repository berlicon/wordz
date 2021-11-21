
CREATE PROCEDURE pr_account_word_ins
(
    @account_id int,
    @word_id int,
    @language_id int
)
AS

IF (NOT EXISTS(SELECT * FROM account_word WHERE account_id = @account_id AND word_id = @word_id AND language_id = @language_id))
    BEGIN
        INSERT INTO account_word (account_id, word_id, language_id)
        VALUES (@account_id, @word_id, @language_id)
    END


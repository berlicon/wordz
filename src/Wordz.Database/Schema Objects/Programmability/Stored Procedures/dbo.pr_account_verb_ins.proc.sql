
CREATE PROCEDURE pr_account_verb_ins
(
    @account_id int,
    @verb_id int
)
AS

IF (NOT EXISTS(SELECT * FROM account_verb WHERE account_id = @account_id AND verb_id = @verb_id))
    BEGIN
        INSERT INTO account_verb (account_id, verb_id)
        VALUES (@account_id, @verb_id)
    END


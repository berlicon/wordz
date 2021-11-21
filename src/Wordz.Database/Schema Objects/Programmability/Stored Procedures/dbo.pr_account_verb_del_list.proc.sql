
CREATE PROCEDURE pr_account_verb_del_list
(
    @account_id int
)
AS

DELETE FROM account_verb
WHERE account_id = @account_id


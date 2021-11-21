
CREATE PROCEDURE dbo.pr_account_get_count
AS

SELECT count(*) as cnt
FROM account



CREATE PROCEDURE dbo.pr_site_get
AS

SELECT id, url
FROM site
ORDER BY id


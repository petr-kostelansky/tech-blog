CREATE PROCEDURE [dbo].[GetArticleTags]
	@ArticleId INT
AS
	SELECT 
		[Code],
		[Name]
	FROM [dbo].[ArticleTag] AS ATag
	LEFT JOIN [dbo].[Tag] AS T 
		ON ATag.TagId = T.Id 
	WHERE ATag.ArticleId = @ArticleId
	OPTION (OPTIMIZE FOR UNKNOWN )

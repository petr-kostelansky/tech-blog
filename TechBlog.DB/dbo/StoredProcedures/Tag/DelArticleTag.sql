CREATE PROCEDURE [dbo].[DelArticleTag]
	@ArticleId	INT,
	@TagCode	VARCHAR(20)
AS
	DECLARE @TagId INT = [dbo].GetTagId(@TagCode)

	DELETE FROM [dbo].[ArticleTag]
	WHERE [TagId] = @TagId AND [ArticleId] = @ArticleId


CREATE PROCEDURE [dbo].[InsArticleTag]
	@ArticleId	INT,
	@TagCode	VARCHAR(20)
AS
	DECLARE @TagId INT = [dbo].GetTagId(@TagCode)

	INSERT INTO [dbo].[ArticleTag] 
	(
		[TagId],
		[ArticleId]
	)
	VALUES 
	(
		@TagId, 
		@ArticleId
	)

RETURN SCOPE_IDENTITY()

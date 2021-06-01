CREATE PROCEDURE [dbo].[InsArticle]
	@Url varchar(250),
	@Title varchar(250),
	@Description varchar(500),
	@Content varchar(max),
	@CreatedByUserId int,
	@MetaDescription varchar(250),
	@MetaKeywords varchar(100)
AS
	INSERT INTO dbo.Article 
	(
		[Url]
		,[Title]
		,[Description]
		,[Content]
		,[Created]
		,LastChange
		,CreatedByUserId
		,MetaDescription
		,MetaKeywords
	)
	VALUES 
	(
		@Url
		,@Title
		,@Description
		,@Content
		,GETDATE()
		,GETDATE()
		,@CreatedByUserId
		,@MetaDescription
		,@MetaKeywords
	)

	RETURN SCOPE_IDENTITY()

	GO

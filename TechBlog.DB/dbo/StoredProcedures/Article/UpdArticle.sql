CREATE PROCEDURE [dbo].[UpdArticle]
	@Id int,
	@Url varchar(250),
	@Title varchar(250),
	@Description varchar(500),
	@Content varchar(max),
	@MetaDescription varchar(250),
	@MetaKeywords varchar(100)
AS

	UPDATE dbo.Article
	SET 
		[Url] = @Url,
		[Title] = @Title,
		[Description] = @Description,
		[Content] = @Content,
		[LastChange] = GETDATE(),
		MetaDescription = @MetaDescription,
		MetaKeywords = @MetaKeywords
	WHERE Id = @Id

	IF(@@ERROR = 0 OR @@ROWCOUNT = 1)
		RETURN 1
	ELSE
		RETURN 0

GO
CREATE TABLE [dbo].[ArticleTag]
(
	[Id] INT NOT NULL Identity(1,1),
	ArticleId INT NOT NULL,
	TagId INT NOT NULL,
	CONSTRAINT PK_ArticleTag PRIMARY KEY (Id),
	CONSTRAINT FK_ArticleTag_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article(Id),
	CONSTRAINT FK_ArticleTag_TagId FOREIGN KEY (TagId) REFERENCES dbo.Tag(Id)
)

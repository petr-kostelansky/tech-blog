CREATE TABLE [dbo].[Article]
(
	[Id] INT NOT NULL Identity(1, 1),
	[Url] varchar(250) NOT NULL,
	[Title] varchar(250) NOT NULL,
	[Description] varchar(500) NULL,
	[Content] varchar(max) NOT NULL,
	[Visible] bit NOT NULL Constraint DF_Article_Visible Default (0),
	[Created] Datetime NOT NULL,
	[LastChange] Datetime NOT NULL,
	[CreatedByUserId] INT NOT NULL,
	Visited	INT NOT NULL CONSTRAINT DF_Article_Visited DEFAULT(0),
	MetaDescription VARCHAR(250) NULL,
	MetaKeywords VARCHAR(100) NULL
	Constraint PK_Article Primary key (Id),
	Constraint FK_Article_UserId Foreign key (CreatedByUserId) References dbo.[User] (Id)
)
GO
CREATE UNIQUE INDEX UX_Article_Url 
ON [Article] ([Url])
INCLUDE(
Title
,[Description]
,Content
,Visible
,Created
,LastChange
,CreatedByUserId
,Visited
,MetaDescription
,MetaKeywords);
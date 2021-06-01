CREATE TABLE [dbo].[User]
(
	[Id]				INT				NOT NULL IDENTITY(1,1),
	[Email]				[nvarchar](256) NOT NULL,
	[PasswordHash]		[nvarchar](max) NOT NULL,
	[SecurityStamp]		[nvarchar](max) NULL,
	[AccessFailedCount] [int]			NULL,
	[UserName]			[nvarchar](256) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
 )

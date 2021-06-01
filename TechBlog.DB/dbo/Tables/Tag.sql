CREATE TABLE [dbo].[Tag]
(
	[Id] INT NOT NULL Identity(1,1),
	Code varchar(50),
	[Name] varchar(100),
	Constraint PK_Tag PRIMARY KEY (Id)
)

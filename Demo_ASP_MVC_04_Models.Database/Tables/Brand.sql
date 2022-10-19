CREATE TABLE [dbo].[Brand]
(
	[Brand_Id] INT NOT NULL IDENTITY ,
	[Name] NVARCHAR(50) NOT NULL,
	[Country] NVARCHAR(50) NULL,
	
	CONSTRAINT PK_Brand PRIMARY KEY([Brand_Id]),
	CONSTRAINT UK_Brand__Name_Country UNIQUE ([Name],[Country])
)

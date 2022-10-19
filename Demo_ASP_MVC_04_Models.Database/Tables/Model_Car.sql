CREATE TABLE [dbo].[Model_Car]
(
	[Model_Car_Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Body_Style] NVARCHAR(50) NULL,
	[Year] INT NULL,
	[Brand_Id] INT,
	
	CONSTRAINT PK_Model_Car 
		PRIMARY KEY([Model_Car_Id]),

	CONSTRAINT FK_Model_Car__Brand 
		FOREIGN KEY([Brand_Id])
		REFERENCES [Brand]([Brand_Id])
)
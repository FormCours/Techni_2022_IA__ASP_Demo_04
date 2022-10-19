CREATE TABLE [dbo].[Engine_Car]
(
	[Engine_Car_Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Power_Type] NVARCHAR(50) NOT NULL

	CONSTRAINT PK_Engine_Car 
		PRIMARY KEY([Engine_Car_Id]),
)

CREATE TABLE [dbo].[Model_Engine_Car]
(
	[Model_Engine_Car_Id] INT NOT NULL IDENTITY,
	[Model_Car_Id] INT NOT NULL,
	[Engine_Car_Id] INT NOT NULL,
	
	CONSTRAINT PK_Model_Engine_Car 
		PRIMARY KEY([Model_Engine_Car_Id]),

	CONSTRAINT FK_Model_Engine_Car__Model
		FOREIGN KEY([Model_Car_Id])
		REFERENCES [Model_Car]([Model_Car_Id]),

	CONSTRAINT FK_Model_Engine_Car__Engine
		FOREIGN KEY([Engine_Car_Id])
		REFERENCES [Engine_Car]([Engine_Car_Id]),

	CONSTRAINT UK_Model_Engine_Car
		UNIQUE ([Model_Car_Id],[Engine_Car_Id])
)

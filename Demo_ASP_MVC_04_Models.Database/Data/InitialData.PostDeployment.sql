-- Autorise d'encoder (explicitement) les valeurs auto-incrementé !
SET IDENTITY_INSERT [Brand] ON;

-- Brand
INSERT INTO [Brand] ([Brand_Id], [Name], [Country])
 VALUES (1, 'Audi', 'Allemagne'),
		(2, 'Polestar', 'Suède');

-- Fin de l'autorisation d'encodage explicite
SET IDENTITY_INSERT [Brand] OFF;


-- Model Car
SET IDENTITY_INSERT [Model_Car] ON;

INSERT INTO [Model_Car]([Model_Car_Id], [Name], [Year], [Body_Style], [Brand_Id])
 VALUES (1, 'Polestar 2', 2020, '5 portes liftback', 2),
		(2, 'Audi A3 (4G)', 1996, '3 ou 5 portes hatchback', 1),
		(3, 'E-tron GT', 2020, '4-door sedan', 1);

SET IDENTITY_INSERT [Model_Car] OFF;


-- Engine Car
SET IDENTITY_INSERT [Engine_Car] ON;

INSERT INTO [Engine_Car]([Engine_Car_Id], [Name], [Power_Type])
 VALUES (1, '1.5 L EA211 TGI Evo turbo I4', 'Essence/CNG'),
		(2, '1.0 L EA211 CHYB turbo', 'Essence'),
		(3, '48 Volt belt-drive alternator starter (MHEV) 81 kW', 'Electrique'),
		(4, 'EFAD + ERAD 300 kW', 'Electrique'),
		(5, '2x AC synchronous electric motors 475 kW', 'Electrique'); 

SET IDENTITY_INSERT [Engine_Car] OFF;

-- Many To Many : Model Car - Engine Car
INSERT INTO [Model_Engine_Car]([Model_Car_Id], [Engine_Car_Id])
 VALUES	(1, 4),
		(2, 1),
		(2, 2),
		(2, 3),
		(3, 5);
	

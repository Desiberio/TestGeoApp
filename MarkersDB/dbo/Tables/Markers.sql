﻿CREATE TABLE [dbo].[Markers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(100),
	[Type] INT NOT NULL,
	[Latitude] DECIMAL(12, 9) NOT NULL,
	[Longtitude] DECIMAL(12, 9) NOT NULL
)
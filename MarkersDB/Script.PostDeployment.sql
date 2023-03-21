if OBJECT_ID(N'dbo.Markers', N'U') is null
begin
	CREATE TABLE [dbo].[Markers]
	(
		[Id] INT NOT NULL PRIMARY KEY IDENTITY,
		[Name] NVARCHAR(50) NOT NULL,
		[Description] NVARCHAR(100),
		[Type] INT NOT NULL,
		[Longtitude] DECIMAL(9, 6) NOT NULL,
		[Latitude] DECIMAL(9, 6) NOT NULL
	);
end
if not exists (select 1 from [dbo].[Markers])
begin
	insert into [dbo].[Markers] (Name, Description, Type, Latitude, Longtitude)
	values ('First', 'First  marker desc', 1, 66.4169575018027, 94.25025752215694),
	('Second', 'Second  marker desc', 2, 67.4169575018027, 94.25025752215694),
	('Third', 'Third  marker desc', 1, 68.4169575018027, 94.25025752215694),
	('Fourth', 'Fourth  marker desc', 1, 69.4169575018027, 94.25025752215694),
	('Fifth', 'Fifth  marker desc', 3, 70.4169575018027, 94.25025752215694);
end
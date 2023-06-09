/*
Deployment script for TestGeoAppMarkersDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TestGeoAppMarkersDB"
:setvar DefaultFilePrefix "TestGeoAppMarkersDB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'The following operation was generated from a refactoring log file 13486dbf-b9f6-4135-ac8d-0a7c5acc780d';

PRINT N'Rename [dbo].[Markers].[Longtitude] to Longitude';


GO
EXECUTE sp_rename @objname = N'[dbo].[Markers].[Longtitude]', @newname = N'Longitude', @objtype = N'COLUMN';


GO
PRINT N'Altering Procedure [dbo].[spMarkers_Get]...';


GO
ALTER PROCEDURE [dbo].[spMarkers_Get]
	@Id int
AS
BEGIN
	select Id, Name, Description, Type, Latitude, Longitude
	from dbo.[Markers]
	where Id = @Id;
END
GO
PRINT N'Altering Procedure [dbo].[spMarkers_GetAll]...';


GO
ALTER PROCEDURE [dbo].[spMarkers_GetAll]
AS
BEGIN
	select Id, Name, Description, Type, Longitude, Latitude
	from dbo.[Markers];
END
GO
PRINT N'Altering Procedure [dbo].[spMarkers_Insert]...';


GO
ALTER PROCEDURE [dbo].[spMarkers_Insert]
	@Name NVARCHAR(50),
	@Description NVARCHAR(100),
	@Type INT = 0,
	@Latitude DECIMAL(12,9),
	@Longitude DECIMAL(12,9)
AS
BEGIN
	insert into dbo.[Markers] (Name, Description, Type, Latitude, Longitude)
	values (@Name, @Description, @Type, @Latitude, @Longitude);
END
GO
PRINT N'Altering Procedure [dbo].[spMarkers_UpdateCoordinates]...';


GO
ALTER PROCEDURE [dbo].[spMarkers_UpdateCoordinates]
	@Id int,
	@Latitude DECIMAL(12,9),
	@Longitude DECIMAL(12,9)
AS
BEGIN
	update dbo.[Markers]
	set Latitude = @Latitude, Longitude = @Longitude
	where Id = @Id;
END
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '13486dbf-b9f6-4135-ac8d-0a7c5acc780d')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('13486dbf-b9f6-4135-ac8d-0a7c5acc780d')

GO

GO
if OBJECT_ID(N'dbo.Markers', N'U') is null
begin
	CREATE TABLE [dbo].[Markers]
	(
		[Id] INT NOT NULL PRIMARY KEY IDENTITY,
		[Name] NVARCHAR(50) NOT NULL,
		[Description] NVARCHAR(100),
		[Type] INT NOT NULL,
		[Longitude] DECIMAL(9, 6) NOT NULL,
		[Latitude] DECIMAL(9, 6) NOT NULL
	);
end
if not exists (select 1 from [dbo].[Markers])
begin
	insert into [dbo].[Markers] (Name, Description, Type, Latitude, Longitude)
	values ('First', 'First  marker desc', 1, 66.4169575018027, 94.25025752215694),
	('Second', 'Second  marker desc', 2, 67.4169575018027, 94.25025752215694),
	('Third', 'Third  marker desc', 1, 68.4169575018027, 94.25025752215694),
	('Fourth', 'Fourth  marker desc', 1, 69.4169575018027, 94.25025752215694),
	('Fifth', 'Fifth  marker desc', 3, 70.4169575018027, 94.25025752215694);
end
GO

GO
PRINT N'Update complete.';


GO

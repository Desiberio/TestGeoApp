CREATE PROCEDURE [dbo].[spMarkers_GetAll]
AS
BEGIN
	select Id, Name, Description, Type, Longitude, Latitude
	from dbo.[Markers];
END
CREATE PROCEDURE [dbo].[spMarkers_GetAll]
AS
BEGIN
	select Id, Name, Description, Type, Longtitude, Latitude
	from dbo.[Markers];
END
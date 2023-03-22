CREATE PROCEDURE [dbo].[spMarkers_Get]
	@Id int
AS
BEGIN
	select Id, Name, Description, Type, Latitude, Longitude
	from dbo.[Markers]
	where Id = @Id;
END
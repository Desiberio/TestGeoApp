CREATE PROCEDURE [dbo].[spMarkers_Get]
	@Id int
AS
BEGIN
	select Id, Name, Description, Type, Latitude, Longtitude
	from dbo.[Markers]
	where Id = @Id;
END
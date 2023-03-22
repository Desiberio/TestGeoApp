CREATE PROCEDURE [dbo].[spMarkers_UpdateCoordinates]
	@Id int,
	@Latitude DECIMAL(12,9),
	@Longitude DECIMAL(12,9)
AS
BEGIN
	update dbo.[Markers]
	set Latitude = @Latitude, Longitude = @Longitude
	where Id = @Id;
END

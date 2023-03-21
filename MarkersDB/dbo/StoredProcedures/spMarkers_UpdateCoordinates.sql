CREATE PROCEDURE [dbo].[spMarkers_UpdateCoordinates]
	@Id int,
	@Latitude DECIMAL(12,9),
	@Longtitude DECIMAL(12,9)
AS
BEGIN
	update dbo.[Markers]
	set Latitude = @Latitude, Longtitude = @Longtitude
	where Id = @Id;
END

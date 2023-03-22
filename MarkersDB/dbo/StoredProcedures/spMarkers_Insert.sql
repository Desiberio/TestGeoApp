CREATE PROCEDURE [dbo].[spMarkers_Insert]
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

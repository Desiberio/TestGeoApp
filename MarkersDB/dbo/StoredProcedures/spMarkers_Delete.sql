CREATE PROCEDURE [dbo].[spMarkers_Delete]
	@Id int
AS
BEGIN
	delete
	from dbo.[Markers]
	where Id = @Id;
END
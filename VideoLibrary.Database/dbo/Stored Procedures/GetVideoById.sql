CREATE PROCEDURE [dbo].[GetVideoById]
	@VideoId INT
AS
BEGIN
	SELECT
		VideoId,
		VideoPath,
		VideoName,
		ThumbnailPath,
		Keywords,
		Rating
	FROM Video WHERE VideoId=@VideoId
END
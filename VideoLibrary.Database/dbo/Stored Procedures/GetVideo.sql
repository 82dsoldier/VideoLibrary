CREATE PROCEDURE [dbo].[GetVideo]
AS
BEGIN
	SELECT
		VideoId,
		VideoPath,
		VideoName,
		ThumbnailPath,
		Keywords,
		Rating
	FROM Video
END
CREATE PROCEDURE [dbo].[InsertVideo]
	@VideoId INT OUTPUT,
	@VideoPath NVARCHAR(256),
	@VideoName NVARCHAR(256),
	@ThumbnailPath NVARCHAR(256),
	@Keywords NVARCHAR(256),
	@Rating SMALLINT
AS
BEGIN
	INSERT INTO Video (VideoPath, VideoName, ThumbnailPath, Keywords, Rating)
	VALUES (@VideoPath, @VideoName, @ThumbnailPath, @Keywords, @Rating)
	SET @VideoId = SCOPE_IDENTITY()
END
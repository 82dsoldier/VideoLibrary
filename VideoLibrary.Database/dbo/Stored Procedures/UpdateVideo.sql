CREATE   PROCEDURE UpdateVideo
    @VideoId INT,
    @VideoPath VARCHAR(256),
    @VideoName VARCHAR(256),
    @ThumbnailPath VARCHAR(256),
    @Keywords VARCHAR(256),
    @Rating INT
AS
BEGIN
    UPDATE Video SET VideoPath=@VideoPath, VideoName=@VideoName, ThumbnailPath=@ThumbnailPath, Keywords=@Keywords, Rating=@Rating
    WHERE VideoId=@VideoId
END
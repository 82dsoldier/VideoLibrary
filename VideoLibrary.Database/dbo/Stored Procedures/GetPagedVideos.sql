CREATE   PROC GetPagedVideos
    @Start INT,
    @Count INT
AS
BEGIN
    SELECT
        VideoID,
        VideoPath,
        VideoName,
        ThumbnailPath,
        Keywords,
        Rating
    FROM Video
    ORDER BY VideoName ASC
    OFFSET @Start ROWS
    FETCH Next @Count ROWS ONLY
END
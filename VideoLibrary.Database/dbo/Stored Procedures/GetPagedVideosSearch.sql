CREATE   PROC GetPagedVideosSearch
    @Keywords VARCHAR(256),
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
    WHERE Keywords LIKE '%' + @Keywords + '%'
    OR VideoName LIKE '%' + @Keywords + '%'
    ORDER BY VideoName ASC
    OFFSET @Start ROWS
    FETCH Next @Count ROWS ONLY
END
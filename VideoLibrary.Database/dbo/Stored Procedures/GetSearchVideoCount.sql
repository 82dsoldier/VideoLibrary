CREATE   PROCEDURE GetSearchVideoCount
    @Keywords NVARCHAR(256)
AS
BEGIN
    SELECT COUNT(VideoId)
    FROM Video
    WHERE Keywords LIKE '%' + @Keywords + '%'
    OR VideoName LIKE '%' + @Keywords + '%'
END
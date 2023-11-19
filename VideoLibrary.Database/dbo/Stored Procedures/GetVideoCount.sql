
CREATE   PROCEDURE GetVideoCount
AS
BEGIN
    SELECT COUNT(VideoId)
    FROM Video
END
﻿CREATE PROCEDURE [dbo].[DoesVideoExist]
	@VideoPath NVARCHAR(MAX)
AS
BEGIN
	SELECT ISNULL((SELECT TOP 1 1 FROM Video WHERE VideoPath=@VideoPath), 0)
END
CREATE TABLE [dbo].[Video] (
    [VideoId]       INT            IDENTITY (1, 1) NOT NULL,
    [VideoPath]     NVARCHAR (256) NULL,
    [VideoName]     NVARCHAR (256) NULL,
    [ThumbnailPath] NVARCHAR (256) NULL,
    [Keywords]      NVARCHAR (256) NULL,
    [Rating]        SMALLINT       DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([VideoId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Video_Keywords]
    ON [dbo].[Video]([Keywords] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Video_VideoPath]
    ON [dbo].[Video]([VideoPath] ASC);


CREATE TABLE [dbo].[AppLog] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [AddedDate]   DATETIME       NOT NULL,
    [Level]       NVARCHAR (10)  NULL,
    [Message]     NVARCHAR (MAX) NULL,
    [StackTrace]  NVARCHAR (MAX) NULL,
    [Exception]   NVARCHAR (MAX) NULL,
    [Logger]      NVARCHAR (255) NULL,
    [RequestUrl]  NVARCHAR (255) NULL,
    [RequestType] NVARCHAR (10)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO


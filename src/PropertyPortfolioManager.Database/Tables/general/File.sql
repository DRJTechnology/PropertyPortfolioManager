CREATE TABLE [general].[File]
(
	[Id]                INT             IDENTITY NOT NULL, 
    [ItemId]            VARCHAR(4000)   NOT NULL, 
    [FileName]          NVARCHAR(500)   NOT NULL, 
	[Size]              BIGINT          NOT NULL, 
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_File_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_File_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([Id] ASC)
)

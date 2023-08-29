CREATE TABLE [general].[Address]
(
	[Id]                INT             IDENTITY NOT NULL, 
    [StreetAddress]     NVARCHAR(250)   NULL, 
    [TownCity]          NVARCHAR(128)   NULL, 
    [CountyRegion]      NVARCHAR(128)   NULL, 
    [PostCode]          NVARCHAR(50)    NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_Address_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_Address_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC)
)

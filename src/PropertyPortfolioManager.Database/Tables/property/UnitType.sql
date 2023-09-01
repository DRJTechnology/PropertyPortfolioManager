CREATE TABLE [property].[UnitType]
(
	[Id]	            SMALLINT		IDENTITY NOT NULL,
	[Type]	            NVARCHAR(255)	NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_UnitType_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_UnitType_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_UnitType] PRIMARY KEY CLUSTERED ([Id] ASC), 
)

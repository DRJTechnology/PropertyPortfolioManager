CREATE TABLE [property].[Tenancy]
(
	[Id]                INT IDENTITY NOT NULL, 
    [TenancyTypeId]     INT NOT NULL, 
    [UnitId]            INT NOT NULL, 
    [StartDate]         DATETIME NULL, 
    [EndDate]           DATETIME NULL, 
    [DurationQuantity]  SMALLINT NULL, 
    [DurationUnitId]    SMALLINT NULL, 
    [ExpireAfterEndDate] BIT NOT NULL DEFAULT 0,
	[Active]            BIT              DEFAULT (1) NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
    [CreateUserId]      INT              NOT NULL,
    [CreateDate]        DATETIME         CONSTRAINT [DF_Tenancy_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT              NOT NULL,
    [AmendDate]         DATETIME         CONSTRAINT [DF_Tenancy_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Tenancy] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Tenancy_TenancyType] FOREIGN KEY ([TenancyTypeId]) REFERENCES [property].[TenancyType]([Id])
)

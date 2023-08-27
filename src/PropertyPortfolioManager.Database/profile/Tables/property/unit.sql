CREATE TABLE [property].[Unit]
(
	[Id]                INT              IDENTITY NOT NULL,
    [Code]              NVARCHAR(50)     NOT NULL,
    [AddressId]         INT              NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
    [CreateUserId]      INT              NOT NULL,
    [CreateDate]        DATETIME         CONSTRAINT [DF_Unit_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT              NOT NULL,
    [AmendDate]         DATETIME         CONSTRAINT [DF_Unit_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Unit_Address] FOREIGN KEY ([AddressId]) REFERENCES [general].[Address]([Id])
)

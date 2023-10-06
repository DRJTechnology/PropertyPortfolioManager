CREATE TABLE [general].[Contact]
(
	[Id]                INT              IDENTITY NOT NULL,
    [PortfolioId]       INT              NOT NULL,
    [ContactTypeId]     INT              NOT NULL,
    [Name]              NVARCHAR(255)    NOT NULL,
    [AddressId]         INT              NULL,
    [Notes]             NVARCHAR(4000)   NULL,
	[Active]            BIT              DEFAULT (1) NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
    [CreateUserId]      INT              NOT NULL,
    [CreateDate]        DATETIME         CONSTRAINT [DF_Unit_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT              NOT NULL,
    [AmendDate]         DATETIME         CONSTRAINT [DF_Unit_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Contact_Portfolio] FOREIGN KEY ([PortfolioId]) REFERENCES [property].[Portfolio]([Id]), 
    CONSTRAINT [FK_Contact_ContactType] FOREIGN KEY ([ContactTypeId]) REFERENCES [general].[ContactType]([Id]), 
    CONSTRAINT [FK_Contact_Address] FOREIGN KEY ([AddressId]) REFERENCES [general].[Address]([Id])
)

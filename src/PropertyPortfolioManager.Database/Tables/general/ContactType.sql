CREATE TABLE [general].[ContactType]
(
	[Id]	            INT		IDENTITY NOT NULL,
    [PortfolioId]       INT             NOT NULL,
	[Type]	            NVARCHAR(255)	NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_ContactType_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_ContactType_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_ContactType_Portfolio] FOREIGN KEY ([PortfolioId]) REFERENCES [property].[Portfolio]([Id]), 
)

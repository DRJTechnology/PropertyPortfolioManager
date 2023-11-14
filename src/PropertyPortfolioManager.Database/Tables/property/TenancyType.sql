CREATE TABLE [property].[TenancyType]
(
	[Id]	            INT		        IDENTITY NOT NULL,
    [PortfolioId]       INT             NOT NULL,
	[Type]	            NVARCHAR(255)	NOT NULL,
	[Active]            BIT             DEFAULT (1) NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_TenancyType_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_TenancyType_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_TenancyType] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_TenancyType_Portfolio] FOREIGN KEY ([PortfolioId]) REFERENCES [property].[Portfolio]([Id]), 
)

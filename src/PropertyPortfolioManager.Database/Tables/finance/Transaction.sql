CREATE TABLE [finance].[Transaction]
(
	[Id]                INT IDENTITY    NOT NULL, 
    [PortfolioId]       INT             NOT NULL,
    [AccountId]         INT             NOT NULL, 
    [TransactionId]     INT             NOT NULL, 
    [Date]              DATETIME        NOT NULL, 
    [Amount]            MONEY           NOT NULL, 
    [Details]           NVARCHAR(1024)  NOT NULL,
    [Direction]         SMALLINT        NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_Transaction_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_Transaction_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Transaction_Portfolio] FOREIGN KEY ([PortfolioId]) REFERENCES [property].[Portfolio]([Id]), 
    CONSTRAINT [FK_Transaction_Account] FOREIGN KEY ([AccountId]) REFERENCES [finance].[Account]([Id])
)

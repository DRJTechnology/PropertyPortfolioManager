CREATE TABLE [finance].[Account]
(
	[Id]                INT              IDENTITY NOT NULL,
    [AccountTypeId]     SMALLINT         NOT NULL,
    [PortfolioId]       INT              NOT NULL,
    [Name]              NVARCHAR(255)    NOT NULL,
    [Notes]             NVARCHAR(4000)   NULL,
	[Active]            BIT              DEFAULT (1) NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
    [CreateUserId]      INT              NOT NULL,
    [CreateDate]        DATETIME         CONSTRAINT [DF_Unit_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT              NOT NULL,
    [AmendDate]         DATETIME         CONSTRAINT [DF_Unit_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Account_Portfolio] FOREIGN KEY ([PortfolioId]) REFERENCES [property].[Portfolio]([Id]), 
    CONSTRAINT [FK_Account_AccountType] FOREIGN KEY ([AccountTypeId]) REFERENCES [finance].[AccountType]([Id])
)

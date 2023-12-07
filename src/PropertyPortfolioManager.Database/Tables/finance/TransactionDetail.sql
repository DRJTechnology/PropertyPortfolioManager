CREATE TABLE [finance].[TransactionDetail]
(
	[Id]                INT IDENTITY    NOT NULL, 
    [TransactionId]     INT             NOT NULL, 
    [AccountId]         INT             NOT NULL, 
    [Date]              DATETIME        NOT NULL, 
    [Description]       NVARCHAR(512)   NOT NULL,
    [Amount]            MONEY           NOT NULL,
    [Direction]         SMALLINT        NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_TransactionDetail_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_TransactionDetail_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_TransactionDetail] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_TransactionDetail_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [finance].[Transaction]([Id]), 
    CONSTRAINT [FK_TransactionDetail_Account] FOREIGN KEY ([AccountId]) REFERENCES [finance].[Account]([Id])
)

CREATE TABLE [finance].[BankAccountDetail]
(
	[Id]                INT              IDENTITY NOT NULL,
  [BankAccountId]     INT              NOT NULL,
  [UploadId]          UNIQUEIDENTIFIER NOT NULL,
  [Date]              DATETIME         NOT NULL, 
  [Amount]            MONEY            NOT NULL,
  [Description]       NVARCHAR(512)    NOT NULL,
  [TransactionType]   NVARCHAR(512)    NOT NULL,
  [TransactionId]     INT              NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
  [CreateUserId]      INT              NOT NULL,
  [CreateDate]        DATETIME         CONSTRAINT [DF_BankAccountDetail_CreateDate] DEFAULT (getutcdate()) NOT NULL,
  [AmendUserId]       INT              NOT NULL,
  [AmendDate]         DATETIME         CONSTRAINT [DF_BankAccountDetail_AmendDate] DEFAULT (getutcdate()) NOT NULL,
  CONSTRAINT [PK_BankAccountDetail] PRIMARY KEY CLUSTERED ([Id] ASC), 
  CONSTRAINT [FK_BankAccountDetail_BankAccount] FOREIGN KEY ([BankAccountId]) REFERENCES [finance].[BankAccount]([Id]), 
  CONSTRAINT [FK_BankAccountDetail_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [finance].[Transaction]([Id])
)

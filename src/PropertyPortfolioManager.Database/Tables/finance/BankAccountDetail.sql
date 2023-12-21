CREATE TABLE [finance].[BankAccountDetail]
(
	[Id]                INT              IDENTITY NOT NULL,
  [AccountId]         INT              NOT NULL,
  [Date]              DATETIME         NOT NULL, 
  [Amount]            MONEY            NOT NULL,
  [Description]       NVARCHAR(512)    NOT NULL,
  [TransactionType]   NVARCHAR(512)    NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
  [CreateUserId]      INT              NOT NULL,
  [CreateDate]        DATETIME         CONSTRAINT [DF_BankAccountDetail_CreateDate] DEFAULT (getutcdate()) NOT NULL,
  [AmendUserId]       INT              NOT NULL,
  [AmendDate]         DATETIME         CONSTRAINT [DF_BankAccountDetail_AmendDate] DEFAULT (getutcdate()) NOT NULL,
  CONSTRAINT [PK_BankAccountDetail] PRIMARY KEY CLUSTERED ([Id] ASC), 
  CONSTRAINT [FK_BankAccountDetail_Account] FOREIGN KEY ([AccountId]) REFERENCES [finance].[Account]([Id])
)

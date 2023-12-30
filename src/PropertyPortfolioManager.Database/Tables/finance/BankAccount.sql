CREATE TABLE [finance].[BankAccount]
(
	[Id]                INT              IDENTITY NOT NULL,
  [AccountId]         INT              NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
  [CreateUserId]      INT              NOT NULL,
  [CreateDate]        DATETIME         CONSTRAINT [DF_BankAccount_CreateDate] DEFAULT (getutcdate()) NOT NULL,
  [AmendUserId]       INT              NOT NULL,
  [AmendDate]         DATETIME         CONSTRAINT [DF_BankAccount_AmendDate] DEFAULT (getutcdate()) NOT NULL,
  CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
)
CREATE TABLE [finance].[AccountType]
(
	[Id]                SMALLINT        NOT NULL,
	[Type]	            NVARCHAR(255)	NOT NULL,
    [CreditDebit]       SMALLINT        NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_AccountType_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_AccountType_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([Id] ASC), 
)

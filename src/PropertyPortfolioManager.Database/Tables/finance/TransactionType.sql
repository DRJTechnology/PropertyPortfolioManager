CREATE TABLE [finance].[TransactionType]
(
	[Id]                SMALLINT        NOT NULL,
	[Type]	            NVARCHAR(255)	NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_TransactionType_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_TransactionType_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED ([Id] ASC), 
)

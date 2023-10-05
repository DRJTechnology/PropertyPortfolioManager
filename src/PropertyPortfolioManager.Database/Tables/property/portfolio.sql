CREATE TABLE [property].[Portfolio]
(
	[Id]                INT              IDENTITY NOT NULL, 
    [Name]              NVARCHAR(128)    NOT NULL,
	[Active]            BIT              DEFAULT (1) NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
    [CreateUserId]      INT              NOT NULL,
    [CreateDate]        DATETIME         CONSTRAINT [DF_Portfolio_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT              NOT NULL,
    [AmendDate]         DATETIME         CONSTRAINT [DF_Portfolio_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED ([Id] ASC)
)

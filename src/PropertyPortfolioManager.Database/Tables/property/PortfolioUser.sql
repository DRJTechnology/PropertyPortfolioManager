CREATE TABLE [property].[PortfolioUser]
(
	[Id]                INT             IDENTITY NOT NULL,
    [PortfolioId]       INT             NOT NULL, 
    [UserId]            INT             NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_PortfolioUser_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_PortfolioUser_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_PortfolioUser] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PortfolioUser_Portfolio] FOREIGN KEY ([PortfolioId]) REFERENCES [property].[Portfolio]([Id]), 
    CONSTRAINT [FK_PortfolioUser_User] FOREIGN KEY ([UserId]) REFERENCES [profile].[User]([Id])
)

CREATE TABLE [profile].[User] (
    [Id]                    INT              NOT NULL IDENTITY,
    [ObjectIdentifier]      UNIQUEIDENTIFIER NOT NULL,
    [Name]                  NVARCHAR (255)   NOT NULL,
    [SelectedPortfolioId]   INT              NULL,
    [Deleted]               BIT              DEFAULT (0) NOT NULL,
    [CreateDate]            DATETIME         CONSTRAINT [DF_User_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendDate]             DATETIME         CONSTRAINT [DF_User_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);


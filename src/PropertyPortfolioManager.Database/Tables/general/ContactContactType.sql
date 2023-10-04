CREATE TABLE [general].[ContactContactType]
(
	[Id]                INT              IDENTITY NOT NULL,
    [ContactId]         INT              NOT NULL,
    [ContactTypeId]     INT              NOT NULL,
	[Deleted]           BIT              DEFAULT (0) NOT NULL,
    [CreateUserId]      INT              NOT NULL,
    [CreateDate]        DATETIME         CONSTRAINT [DF_ContactContactType_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT              NOT NULL,
    [AmendDate]         DATETIME         CONSTRAINT [DF_ContactContactType_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_ContactContactType] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_ContactContactType_Contact] FOREIGN KEY ([ContactId]) REFERENCES [general].[Contact]([Id]), 
    CONSTRAINT [FK_ContactContactType_ContactType] FOREIGN KEY ([ContactTypeId]) REFERENCES [general].[ContactType]([Id]), 
)

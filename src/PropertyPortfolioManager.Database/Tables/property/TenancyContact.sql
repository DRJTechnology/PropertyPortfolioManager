CREATE TABLE [property].[TenancyContact]
(
	[Id]                INT IDENTITY    NOT NULL, 
    [TenancyId]         INT             NOT NULL, 
    [ContactId]         INT             NOT NULL,
	[Deleted]           BIT             DEFAULT (0) NOT NULL,
    [CreateUserId]      INT             NOT NULL,
    [CreateDate]        DATETIME        CONSTRAINT [DF_TenancyContact_CreateDate] DEFAULT (getutcdate()) NOT NULL,
    [AmendUserId]       INT             NOT NULL,
    [AmendDate]         DATETIME        CONSTRAINT [DF_TenancyContact_AmendDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_TenancyContact] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_TenancyContact_Tenancy] FOREIGN KEY ([TenancyId]) REFERENCES [property].[Tenancy]([Id]), 
    CONSTRAINT [FK_TenancyContact_Contact] FOREIGN KEY ([ContactId]) REFERENCES [general].[Contact]([Id])
)

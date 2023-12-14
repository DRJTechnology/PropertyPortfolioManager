CREATE TABLE [dbo].[CombinedAccounts] (
    [Date]           DATE            NOT NULL,
    [Transaction_ID] SMALLINT        NOT NULL,
    [Ledger]         NVARCHAR (250)  NOT NULL,
    [Description]    NVARCHAR (4000) NOT NULL,
    [Reference]      NVARCHAR (250)  NULL,
    [Property]       NVARCHAR (250)  NULL,
    [Room]           NVARCHAR (1)    NULL,
    [Debit]          MONEY           NULL,
    [Credit]         MONEY           NULL,
    [Balance]        MONEY           NOT NULL,
    [Original_Order] SMALLINT        NOT NULL,
    [selected]       NVARCHAR (1)    NULL
);
GO



--	Populate internal portfolio - Start
IF NOT EXISTS(SELECT 1 FROM [property].[Portfolio] WHERE id = -1)
BEGIN
	SET IDENTITY_INSERT [property].[Portfolio] ON 
    
    INSERT [property].[Portfolio] (id, [Name], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (-1, N'Internal', 0, 1, SYSDATETIME(), 1, SYSDATETIME())

	DBCC CHECKIDENT ('property.Portfolio', RESEED, 1)

	SET IDENTITY_INSERT [property].[Portfolio] OFF
END	
--	Populate internal portfolio - End


--	Populate Contact Types - Start

IF NOT EXISTS(SELECT 1 FROM [general].[ContactType] WHERE id = 1)
BEGIN
	SET IDENTITY_INSERT [general].[ContactType] ON 
	INSERT INTO [general].[ContactType] (Id, PortfolioId, [Type], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (1, -1, 'Tenant', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [general].[ContactType] OFF
END

IF NOT EXISTS(SELECT 1 FROM [general].[ContactType] WHERE id = 2)
BEGIN
	SET IDENTITY_INSERT [general].[ContactType] ON 
	INSERT INTO [general].[ContactType] (Id, PortfolioId, [Type], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (2, -1, 'Letting Agent', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [general].[ContactType] OFF
END

IF NOT EXISTS(SELECT 1 FROM [general].[ContactType] WHERE id = 3)
BEGIN
	SET IDENTITY_INSERT [general].[ContactType] ON 
	INSERT INTO [general].[ContactType] (Id, PortfolioId, [Type], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (3, -1, 'Supplier', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [general].[ContactType] OFF
END

IF NOT EXISTS(SELECT 1 FROM [general].[ContactType] WHERE id = 4)
BEGIN
	SET IDENTITY_INSERT [general].[ContactType] ON 
	INSERT INTO [general].[ContactType] (Id, PortfolioId, [Type], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (4, -1, 'Finance Provider', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [general].[ContactType] OFF
END

--	Populate Contact Types - End

-- Populate finance Transaction Types - Start
IF NOT EXISTS(SELECT 1 FROM [finance].[TransactionType] WHERE Id = 1)
BEGIN
	INSERT INTO [finance].[TransactionType] (Id, [Type], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (1, 'Journal', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
-- Populate finance Transaction Types - End

-- Populate finance Account Types - Start
IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 1)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (1, 'Asset', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 2)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (2, 'Liability', -1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 3)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (3, 'Revenue', -1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 4)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (4, 'Expense', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 5)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (5, 'Equity', -1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 6)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (6, 'Long-term Liability', -1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

IF NOT EXISTS(SELECT 1 FROM [finance].[AccountType] WHERE id = 7)
BEGIN
	INSERT INTO [finance].[AccountType] (Id, [Type], [CreditDebit], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (7, 'Bank Account', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
-- Populate finance Account Types - End

-- Populate initial finance Accounts - Start
IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 1)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (1, 2, -1, 'Accounts Payable', 'Current liabilities', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 2)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (2, 1, -1, 'Accounts Receivable', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 3)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (3, 4, -1, 'Bad Debt', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 4)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (4, 1, -1, 'Fixed Assets', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 5)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (5, 4, -1, 'Legal Fees', 'Solicitors'' fees', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 6)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (6, 4, -1, 'Professional Fees', 'Accountancy, tax, architect and other professional fees', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 7)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (7, 4, -1, 'Property Management Fees', 'Charges incurred from letting and property management companies', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 8)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (8, 4, -1, 'Mortgage & Loan Interest', 'Interest charged by mortgage and loan companies', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 9)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (9, 2, -1, 'Mispostings', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 10)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (10, 3, -1, 'Banking Income', 'Income received from bank e.g. interest', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 11)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (11, 3, -1, 'Other Income', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 12)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (12, 3, -1, 'Rental Income', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 13)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (13, 4, -1, 'Property Repairs', 'Property related repairs', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 14)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (14, 1, -1, 'Property Selling Costs', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 15)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (15, 4, -1, 'Insurances', 'Insurance costs', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 16)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (16, 5, -1, 'Retained Earnings', '', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 17)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (17, 4, -1, 'Services Provided', 'Property services such as gardening, cleaning, etc.', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END

IF NOT EXISTS(SELECT 1 FROM [finance].[Account] WHERE id = 18)
BEGIN
	SET IDENTITY_INSERT [finance].[Account] ON 
	INSERT INTO [finance].[Account] ([Id], [AccountTypeId], [PortfolioId], [Name], [Notes], [Active], [Deleted], [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES (18, 1, -1, 'Stamp Duty Land Tax', 'Stamp duty land tax to HMRC', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	SET IDENTITY_INSERT [finance].[Account] OFF
END
-- Populate initial finance Accounts - End

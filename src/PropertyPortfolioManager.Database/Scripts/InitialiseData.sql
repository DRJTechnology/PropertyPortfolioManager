
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


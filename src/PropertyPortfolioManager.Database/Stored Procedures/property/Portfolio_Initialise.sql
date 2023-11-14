-- =================================================================
-- Author:		Dave Brown
-- Create date: 01 Oct 2023
-- Description:	Creates the base lookup data required by a protfolio
-- =================================================================
CREATE PROCEDURE [property].[Portfolio_Initialise]
	@PortfolioId	int,
	@CurrentUserId	int
AS

	/* Unit Types - start */
	IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE PortfolioId = @PortfolioId AND [Type] = 'Detached House')
	BEGIN
		INSERT INTO property.UnitType (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@PortfolioId, 'Detached House', 1, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
	IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE PortfolioId = @PortfolioId AND [Type] = 'Semi-Detached House')
	BEGIN
		INSERT INTO property.UnitType (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@PortfolioId, 'Semi-Detached House', 1, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
	IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE PortfolioId = @PortfolioId AND [Type] = 'Detached Bungalow')
	BEGIN
		INSERT INTO property.UnitType (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@PortfolioId, 'Detached Bungalow', 1, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
	IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE PortfolioId = @PortfolioId AND [Type] = 'Semi-Detached Bungalow')
	BEGIN
		INSERT INTO property.UnitType (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@PortfolioId, 'Semi-Detached Bungalow', 1, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
	IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE PortfolioId = @PortfolioId AND [Type] = 'Flat')
	BEGIN
		INSERT INTO property.UnitType (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@PortfolioId, 'Flat', 1, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
	IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE PortfolioId = @PortfolioId AND [Type] = 'Terraced House')
	BEGIN
		INSERT INTO property.UnitType (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (@PortfolioId, 'Terraced House', 1, 0, @CurrentUserId, SYSDATETIME(), @CurrentUserId, SYSDATETIME())
	END
	/* Unit Types - end */

  
	/* Tenancy Types - start */
	IF NOT EXISTS (SELECT 1 FROM property.[TenancyType] WHERE PortfolioId = 2 AND [Type] = 'Assured Shorthold Tenancy (AST)')
	BEGIN
		INSERT INTO property.[TenancyType] (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (2, 'Assured Shorthold Tenancy (AST)', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	END
	IF NOT EXISTS (SELECT 1 FROM property.[TenancyType] WHERE PortfolioId = 2 AND [Type] = 'Company Let Agreement')
	BEGIN
		INSERT INTO property.[TenancyType] (PortfolioId, [Type], Active, Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
		VALUES (2, 'Company Let Agreement', 1, 0, 1, SYSDATETIME(), 1, SYSDATETIME())
	END
	/* Tenancy Types - end */


RETURN 0

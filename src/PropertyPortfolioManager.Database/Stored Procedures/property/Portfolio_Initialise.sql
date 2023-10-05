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

RETURN 0

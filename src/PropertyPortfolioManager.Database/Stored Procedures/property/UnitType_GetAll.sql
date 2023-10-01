﻿-- =============================================
-- Author:		Dave Brown
-- Create date: 30 Sep 2023
-- Description:	Get all property unit type records
-- =============================================
CREATE PROCEDURE [property].[UnitType_GetAll]
	@PortfolioId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type]
	FROM	[property].[UnitType]
	Where	PortfolioId = @PortfolioId
		AND Deleted = 0
	ORDER BY [Type]

END
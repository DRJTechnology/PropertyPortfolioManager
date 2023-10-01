-- ================================================
-- Author:		Dave Brown
-- Create date: 30 Sep 2023
-- Description:	Updates a property unit type record
-- ================================================
CREATE PROCEDURE [property].[UnitType_Update]
	@Id					INT, 
	@PortfolioId		INT,
	@Type				NVARCHAR(255),
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	[property].[UnitType]
	SET		[Type] = @Type,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	WHERE	PortfolioId = @PortfolioId AND Id = @Id

END
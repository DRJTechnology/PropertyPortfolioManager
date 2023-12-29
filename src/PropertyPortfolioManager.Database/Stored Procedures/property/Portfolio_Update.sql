-- ================================================
-- Author:		Dave Brown
-- Create date: 30 Sep 2023
-- Description:	Updates a property portfolio record
-- ================================================
CREATE PROCEDURE [property].[Portfolio_Update]
	@Id					INT, 
	@Name				NVARCHAR(255),
	@Active				BIT,
	@CurrentUserId		INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	p
	SET		[Name] = @Name,
			Active = @Active,
			AmendUserId = @CurrentUserId,
			AmendDate = SYSDATETIME()
	FROM	[property].[Portfolio] p
	INNER JOIN property.PortfolioUser pu on p.Id = pu.PortfolioId AND pu.UserId = @CurrentUserId AND pu.Deleted = 0
	WHERE	p.Id = @Id
		AND p.Deleted = 0

END
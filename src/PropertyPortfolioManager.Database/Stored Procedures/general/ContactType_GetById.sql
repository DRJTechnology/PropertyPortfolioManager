-- ======================================
-- Author:		Dave Brown
-- Create date: 03 Oct 2023
-- Description:	Get a contact type record
-- ======================================
CREATE PROCEDURE [general].[ContactType_GetById]
	@Id				INT,
	@PortfolioId	INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT	Id, [Type], Deleted, CreateUserId, CreateDate, AmendUserId, AmendDate
	FROM	[general].[ContactType]
	WHERE	(PortfolioId = @PortfolioId OR PortfolioId = -1)
		AND Id = @Id

END
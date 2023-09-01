-- Populate lookup tables

/* Unit Types - start */
IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE [Type] = 'Detached House')
BEGIN
	INSERT INTO property.UnitType ([Type], Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES ('Detached House', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE [Type] = 'Semi-Detached House')
BEGIN
	INSERT INTO property.UnitType ([Type], Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES ('Semi-Detached House', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE [Type] = 'Detached Bungalow')
BEGIN
	INSERT INTO property.UnitType ([Type], Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES ('Detached Bungalow', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE [Type] = 'Semi-Detached Bungalow')
BEGIN
	INSERT INTO property.UnitType ([Type], Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES ('Semi-Detached Bungalow', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE [Type] = 'Flat')
BEGIN
	INSERT INTO property.UnitType ([Type], Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES ('Flat', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END
IF NOT EXISTS (SELECT 1 FROM property.UnitType WHERE [Type] = 'Terraced House')
BEGIN
	INSERT INTO property.UnitType ([Type], Deleted, [CreateUserId], [CreateDate], [AmendUserId], [AmendDate])
	VALUES ('Terraced House', 0, 1, SYSDATETIME(), 1, SYSDATETIME())
END

/* Unit Types - end */

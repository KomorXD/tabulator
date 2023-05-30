-- Generowanie losowych danych dla tabeli DepartmentRooms
INSERT INTO [dbo].[DepartmentRooms] ([DepartmentId], [RoomId])
SELECT
    ABS(CHECKSUM(NEWID())) % (123 - 24 + 1) + 24 AS DepartmentId,
    Id AS RoomId
FROM
    [dbo].[Rooms]
WHERE
    Id BETWEEN 46 AND 146

-- Generowanie losowych danych dla tabeli FacultyRooms
INSERT INTO [dbo].[FacultyRooms] ([FacultyId], [RoomId])
SELECT
    ABS(CHECKSUM(NEWID())) % (42 - 22 + 1) + 22 AS FacultyId,
    Id AS RoomId
FROM
    [dbo].[Rooms]
WHERE
    Id BETWEEN 46 AND 146
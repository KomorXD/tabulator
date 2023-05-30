INSERT INTO [dbo].[EquipmentCaretakers] ([EmployeeId], [EquipmentId])
SELECT
    [Employee].[Id] AS [EmployeeId],
    [Equipment].[Id] AS [EquipmentId]
FROM
    (
        SELECT
            [Id],
            ROW_NUMBER() OVER (ORDER BY NEWID()) AS [RowNumber]
        FROM
            [dbo].[Employee]
        WHERE
            [Id] BETWEEN 69 AND 294
    ) AS [Employee]
JOIN
    (
        SELECT
            [Id],
            ROW_NUMBER() OVER (ORDER BY NEWID()) AS [RowNumber]
        FROM
            [dbo].[Equipment]
        WHERE
            [Id] BETWEEN 578 AND 805
    ) AS [Equipment]
ON
    [Employee].[RowNumber] = [Equipment].[RowNumber]
GO
-- Wypełnienie tabeli FacultyTechEmployee

WITH CTE_Faculty AS (
    SELECT
        f.Id AS FacultyId,
        e.Id AS EmployeeId,
        ROW_NUMBER() OVER (PARTITION BY f.Id ORDER BY NEWID()) AS RowNum
    FROM
        (SELECT Id FROM Faculties WHERE Id BETWEEN 22 AND 42) f
    CROSS JOIN
        (SELECT Id FROM Employee WHERE Id BETWEEN 69 AND 294) e
    LEFT JOIN
        FacultyTechEmployee fte ON fte.FacultyId = f.Id AND fte.EmployeeId = e.Id
    WHERE
        fte.EmployeeId IS NULL
)
INSERT INTO FacultyTechEmployee (EmployeeId, FacultyId)
SELECT
    EmployeeId,
    FacultyId
FROM
    CTE_Faculty
WHERE
    RowNum = 1;

-- Wypełnienie tabeli DepartmentTechEmployee

WITH CTE_Department AS (
    SELECT
        d.Id AS DepartmentId,
        e.Id AS EmployeeId,
        ROW_NUMBER() OVER (PARTITION BY d.Id ORDER BY NEWID()) AS RowNum
    FROM
        (SELECT Id FROM Department WHERE Id BETWEEN 24 AND 123) d
    CROSS JOIN
        (SELECT Id FROM Employee WHERE Id BETWEEN 69 AND 294) e
    LEFT JOIN
        DepartmentTechEmployee dte ON dte.DepartmentId = d.Id AND dte.EmployeeId = e.Id
    WHERE
        dte.EmployeeId IS NULL
)
INSERT INTO DepartmentTechEmployee (EmployeeId, DepartmentId)
SELECT
    EmployeeId,
    DepartmentId
FROM
    CTE_Department
WHERE
    RowNum = 1;
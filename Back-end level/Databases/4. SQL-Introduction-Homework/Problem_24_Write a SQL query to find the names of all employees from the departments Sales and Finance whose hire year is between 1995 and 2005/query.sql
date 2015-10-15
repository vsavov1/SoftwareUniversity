SELECT e.FirstName + ' ' + e.LastName AS 'FullName'
FROM Employees e
INNER JOIN Departments d
ON (e.DepartmentID = d.DepartmentID) 
	AND d.Name IN ('Sales', 'Finane')
WHERE e.HireDate BETWEEN '1995-01-01' AND '2005-12-31'
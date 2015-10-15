SELECT e.FirstName + ' ' + e.LastName as  'EmployeName', 
ISNULL(m.FirstName + ' ' + m.LastName,NULL) as 'MangerName'
FROM Employees e
LEFT OUTER JOIN Employees m 
ON e.EmployeeID = m.ManagerID



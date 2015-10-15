SELECT * FROM Employees e
JOIN Employees m
ON (e.ManagerId = m.EmployeeId)
JOIN Addresses a
ON m.AddressID = a.AddressID
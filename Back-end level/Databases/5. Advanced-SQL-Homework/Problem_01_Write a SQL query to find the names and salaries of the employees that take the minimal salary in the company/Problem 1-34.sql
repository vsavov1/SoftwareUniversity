--Problem 1.	Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company.
SELECT FirstName, LastName, Salary 
FROM Employees e
WHERE e.Salary = (
	SELECT MIN(Salary) 
	FROM Employees
)
--Problem 1.	Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company.
--
--
--Problem 2.	Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% higher than the minimal salary for the company.
SELECT FirstName, LastName, Salary
FROM Employees e
WHERE e.Salary <= (
	SELECT MIN(Salary * 1.10)
	FROM Employees
)
--Problem 2.	Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% higher than the minimal salary for the company.
--
--
--Problem 3.	Write a SQL query to find the full name, salary and department of the employees that take the minimal salary in their department.
SELECT FirstName + ' ' +  LastName AS FullName, Salary, d.Name AS Department
FROM Employees e

JOIN Departments d 
ON d.DepartmentID = e.DepartmentID

WHERE e.Salary = (
	SELECT MIN(Salary) 
	FROM Employees 
	WHERE  DepartmentID = e.DepartmentID
)
--Problem 3.	Write a SQL query to find the full name, salary and department of the employees that take the minimal salary in their department.
--
--
--Problem 4.	Write a SQL query to find the average salary in the department #1.
SELECT  AVG(Salary) AS AverageSalary
FROM Employees e
WHERE e.DepartmentID = 1
--Problem 4.	Write a SQL query to find the average salary in the department #1.
--
--
--Problem 5.	Write a SQL query to find the average salary in the "Sales" department.
SELECT  AVG(Salary) as 'Average Salary for Sales Department'
FROM Employees e

JOIN Departments d
ON d.DepartmentID = e.DepartmentID

WHERE d.Name = 'Sales'
--Problem 5.	Write a SQL query to find the average salary in the "Sales" department.
--
--
--Problem 6.	Write a SQL query to find the number of employees in the "Sales" department.
SELECT  Count(EmployeeID) as 'Sales Employees Count'
FROM Employees e

JOIN Departments d
ON d.DepartmentID = e.DepartmentID

WHERE d.Name = 'Sales'
--Problem 6.	Write a SQL query to find the number of employees in the "Sales" department.
--
--
--Problem 7.	Write a SQL query to find the number of all employees that have manager.
SELECT  Count(*) as 'Employees with manager'
FROM Employees e
WHERE e.ManagerID IS NOT NULL
--Problem 7.	Write a SQL query to find the number of all employees that have manager.
--
--
--Problem 8.	Write a SQL query to find the number of all employees that have no manager.
SELECT  Count(*) as 'Employees without manager'
FROM Employees e
WHERE e.ManagerID IS NULL
--Problem 8.	Write a SQL query to find the number of all employees that have no manager.
--
--
--Problem 9.	Write a SQL query to find all departments and the average salary for each of them.
SELECT d.Name as 'Department', AVG(e.Salary) as 'Average Salary'
FROM Employees e

JOIN Departments d
ON d.DepartmentID = e.DepartmentID

GROUP BY d.Name
--Problem 9.	Write a SQL query to find all departments and the average salary for each of them.
--
--
--Problem 10.	Write a SQL query to find the count of all employees in each department and for each town. 
SELECT t.Name, d.Name as 'Department', COUNT(e.EmployeeID) as 'Employees count'
FROM Employees e

JOIN Departments d
ON d.DepartmentID = e.DepartmentID

JOIN Addresses a
ON a.AddressID = e.AddressID

JOIN Towns t
ON t.TownID = a.TownID

GROUP BY t.Name, d.Name
--Problem 10.	Write a SQL query to find the count of all employees in each department and for each town. 
--
--
--Problem 11.	Write a SQL query to find all managers that have exactly 5 employees.
SELECT m.FirstName, m.LastName, COUNT(E.EmployeeID) AS 'Employees count'
FROM Employees e
JOIN Employees m
ON e.ManagerID = m.EmployeeID
GROUP BY m.FirstName, m.LastName
HAVING COUNT(e.EmployeeID) = 5
--Problem 11.	Write a SQL query to find all managers that have exactly 5 employees.
--
--
--Problem 12.	Write a SQL query to find all employees along with their managers.
SELECT e.FirstName + ' ' + e.LastName, 
ISNULL(m.FirstName + ' ' + m.LastName, '(no manager)')
FROM Employees e
LEFT OUTER JOIN Employees m
ON m.EmployeeID = e.ManagerID
--Problem 12.	Write a SQL query to find all employees along with their managers.
--
--
--Problem 13.	Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. 
SELECT e.FirstName, e.LastName
FROM Employees e
WHERE LEN(e.LastName) = 5
--Problem 13.	Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. 
--
--
--Problem 14.	Write a SQL query to display the current date and time in the following format "day.month.year hour:minutes:seconds:milliseconds". 
SELECT CONVERT(VARCHAR(24),GETDATE(),113)
--Problem 14.	Write a SQL query to display the current date and time in the following format "day.month.year hour:minutes:seconds:milliseconds". 
--
--
--Problem 15.	Write a SQL statement to create a table Users.
CREATE TABLE Users(
	UserID int IDENTITY,
	Username nvarchar(50) UNIQUE NOT NULL,
	Password nvarchar(50), -- if we use SHA-512 hasing we must use binary type 
	FullName nvarchar(50) NOT NULL,
	LastLogin date,
	CONSTRAINT PK_UserId PRIMARY KEY(UserID),
	CHECK (LEN(Password) BETWEEN 5 and 25)
)
INSERT INTO Users VALUES
('Peshooosexa','123123123', 'Pepo mepo', GETDATE()),
('nakovvvvvsexa','123123123', 'Nako bako', GETDATE()),
('slavipleshiviq','123123123', 'Slavi drenkata', GETDATE()),
('bubata','123123123', 'guga muga', GETDATE()),
('piji','123123123', 'djidji bidji', GETDATE()),
('novUser','123123123', 'noviq userFullName', GETDATE())
--Problem 15.	Write a SQL statement to create a table Users.
--
--
--Problem 16.	Write a SQL statement to create a view that displays the users from the Users table that have been in the system today.
GO
CREATE VIEW [todayUsers]  AS
SELECT Username, FullName
FROM Users
WHERE LastLogin = CONVERT(date, GETDATE())
-- test view
--GO
--SELECT * FROM todayUsers
--Problem 16.	Write a SQL statement to create a view that displays the users from the Users table that have been in the system today.
--
--
--Problem 17.	Write a SQL statement to create a table Groups. 
GO
CREATE TABLE Groups(
	GroupId int IDENTITY,
	Name nvarchar(50) UNIQUE NOT NULL,
	CONSTRAINT PK_GroupId 
	PRIMARY KEY(GroupId)
)
--
--INSERT INTO Groups VALUES 
--('bageristi'),
--('programisti'),
--('nosql'),
--('sql')
--
--Problem 17.	Write a SQL statement to create a table Groups. 
--
--
--Problem 18.	Write a SQL statement to add a column GroupID to the table Users.
GO
ALTER TABLE Users
ADD GroupId int
CONSTRAINT FK_Users_Groups
FOREIGN KEY (GroupId)
REFERENCES Groups(GroupId)
--
--UPDATE Users
--SET GroupId = 1
--WHERE UserID = 4
--UPDATE Users
--SET GroupId = 2
--WHERE UserID = 7
--UPDATE Users
--SET GroupId = 2
--WHERE UserID = 11
--UPDATE Users
--SET GroupId = 2
--WHERE UserID = 9
--UPDATE Users
--SET GroupId = 3
--WHERE UserID = 8
--
--Problem 18.	Write a SQL statement to add a column GroupID to the table Users.
--
--
--Problem 19.	Write SQL statements to insert several records in the Users and Groups tables.
INSERT INTO Users VALUES
('Peshooosexa','123123123', 'Pepo mepo', GETDATE(), 1),
('nakovvvvvsexa','123123123', 'Nako bako', GETDATE(), 1),
('slavipleshiviq','123123123', 'Slavi drenkata', GETDATE(), 1),
('bubata','123123123', 'guga muga', GETDATE(), 2),
('piji','123123123', 'djidji bidji', GETDATE(), 2),
('novUser','123123123', 'noviq userFullName', GETDATE(), 3)

INSERT INTO Groups VALUES
('bageristi'),
('programisti'),
('nosql'),
('sql')
--Problem 19.	Write SQL statements to insert several records in the Users and Groups tables.
--
--
--Problem 20.	Write SQL statements to update some of the records in the Users and Groups tables.
UPDATE Users
SET FullName = 'ebasiImeto'
WHERE UserID = 8

UPDATE Groups
SET Name = 'ebasiGrupata'
WHERE GroupID = 4
--Problem 20.	Write SQL statements to update some of the records in the Users and Groups tables.
--
--
--Problem 21.	Write SQL statements to delete some of the records from the Users and Groups tables.
DELETE FROM Users
WHERE UserID = 10 

DELETE FROM Groups
WHERE GroupId = 4
--Problem 21.	Write SQL statements to delete some of the records from the Users and Groups tables.
--
--
--Problem 22.	Write SQL statements to insert in the Users table the names of all employees from the Employees table.
INSERT INTO Users(FullName, Username, Password)
SELECT 
	e.FirstName + ' ' + e.LastName, 
	LOWER(LEFT(e.FirstName, 1) + e.LastName + RIGHT(e.FirstName, 2)), -- Added one RIGHT... because conflict with constraint for unique username
	LOWER(LEFT(e.FirstName, 5) + e.LastName)
FROM Employees e
--Problem 22.	Write SQL statements to insert in the Users table the names of all employees from the Employees table.
--
--
--Problem 23.	Write a SQL statement that changes the password to NULL for all users that have not been in the system since 10.03.2010.
--Test User
INSERT INTO Users (Username, Password, FullName, LastLogin, GroupID)
VALUES('novUser','123123123', 'noviq userFullName', '01.01.2010', 3)
--Test User

UPDATE USERS
SET Password = NULL
WHERE LastLogin < '10.03.2010'
--Problem 23.	Write a SQL statement that changes the password to NULL for all users that have not been in the system since 10.03.2010.
--
--
--Problem 24.	Write a SQL statement that deletes all users without passwords (NULL password).
DELETE FROM Users
WHERE Password IS NULL
--Problem 24.	Write a SQL statement that deletes all users without passwords (NULL password).
--
--
--Problem 25.	Write a SQL query to display the average employee salary by department and job title.
SELECT d.Name AS 'Department', e.JobTitle, AVG(e.Salary) AS 'Average Salary'
FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle 
--Problem 25.	Write a SQL query to display the average employee salary by department and job title.
--
--
--Problem 26.	Write a SQL query to display the minimal employee salary by department and job title along with the name of some of the employees that take it.
SELECT d.Name AS 'Department', e.JobTitle, e.FirstName as 'First Name', MIN(e.Salary) AS 'Min Salary'
FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
WHERE e.Salary = (
	SELECT MIN(Salary)
	FROM Employees
	WHERE DepartmentID = e.DepartmentID
	AND JobTitle = e.JobTitle
)
GROUP BY  d.Name, e.JobTitle, e.FirstName
--Problem 26.	Write a SQL query to display the minimal employee salary by department and job title along with the name of some of the employees that take it.
--
--
--Problem 27.	Write a SQL query to display the town where maximal number of employees work.
SELECT TOP 1 t.Name, COUNT(a.TownID) AS 'Number of employees'
FROM Employees e

JOIN Addresses a
ON e.AddressID = a.AddressID

JOIN Towns t
ON t.TownID = a.TownID

GROUP BY t.Name
ORDER BY 'Number of employees' DESC
--Problem 27.	Write a SQL query to display the town where maximal number of employees work.
--
--
--Problem 28.	Write a SQL query to display the number of managers from each town.
SELECT t.Name, COUNT(DISTINCT M.EmployeeID) AS 'Number of managers'
FROM Employees e

JOIN Addresses a
ON e.AddressID = a.AddressID

JOIN Towns t
ON t.TownID = a.TownID

JOIN Employees m
ON e.ManagerID = m.EmployeeID

GROUP BY t.Name
--Problem 28.	Write a SQL query to display the number of managers from each town.
--
--
--Problem 29.	Write a SQL to create table WorkHours to store work reports for each employee.
CREATE TABLE WorkHours (
	WorkHoursId INT IDENTITY NOT NULL,
	Date DATE NOT NULL,
	Task NVARCHAR(200) NOT NULL,
	Hours INT NOT NULL,
	Comments NVARCHAR(MAX),
	EmployeeID INT NOT NULL
	CONSTRAINT PK_WorkHours PRIMARY KEY (WorkHoursId)
	CONSTRAINT FK_WorkHours_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
)
--Problem 29.	Write a SQL to create table WorkHours to store work reports for each employee.
--
--
--Problem 30.	Issue few SQL statements to insert, update and delete of some data in the table.
INSERT INTO WorkHours VALUES
(GETDATE(),'new task4',3,'daaqfhfghdsadad',3),
(GETDATE(),'new task3',3,'65765h7',20),
(GETDATE(),'new task2',3,'gq4a6h76u',30),
(GETDATE(),'new task1',3,'fhgfhf',50),
(GETDATE(),'new task',3,'dsadasdasdsadad',3)

UPDATE WorkHours
SET Task = 'upadate task'
WHERE WorkHoursId = 1

DELETE WorkHours
WHERE WorkHoursId = 1
--Problem 30.	Issue few SQL statements to insert, update and delete of some data in the table.
--
--
--Problem 31.	Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
CREATE TABLE WorkHoursLogs (
	LogId INT IDENTITY,
	Type NVARCHAR(50) NOT NULL,
	WorkHoursId INT NOT NULL,
	Date DATE NOT NULL,
	Task NVARCHAR(200) NOT NULL,
	Hours INT NOT NULL,
	Comments NVARCHAR(MAX),
	EmployeeID INT NOT NULL
	CONSTRAINT PK_LogId PRIMARY KEY (LogId)
	CONSTRAINT FK_WorkHours_Employeess FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
)

GO
----------------------------------
CREATE TRIGGER WorkHourTriggerUpdateDelete
ON WorkHours
FOR UPDATE, DELETE
AS
DECLARE @type nvarchar(50), @WorkHoursId int, @Date DATE, @TASK NVARCHAR(200), @Hours INT, @Comments NVARCHAR(MAX), @EmploeeID INT

IF exists (SELECT * FROM inserted)
		SELECT @type = 'UPDATED'
	ELSE
		SELECT @type = 'DELETED'

INSERT WorkHoursLogs(Type,WorkHoursId, Date, Task, Hours, Comments, EmployeeID)
SELECT @type, WorkHoursId, Date, Task, Hours, Comments, EmployeeID 
FROM deleted
----------------------------------
GO
CREATE TRIGGER WorkHourTriggerInsert
ON WorkHours
FOR INSERT
AS
DECLARE @type nvarchar(50), @WorkHoursId int, @Date DATE, @TASK NVARCHAR(200), @Hours INT, @Comments NVARCHAR(MAX), @EmploeeID INT
SELECT @type = 'INSERTED'
INSERT WorkHoursLogs(Type,WorkHoursId, Date, Task, Hours, Comments, EmployeeID)
SELECT @type, WorkHoursId, Date, Task, Hours, Comments, EmployeeID 
FROM inserted
----------------------------------

-- TEST TRIGGER
INSERT INTO WorkHours VALUES
(GETDATE(),'new task6', 3 ,'daaqfhfadsadsadad',3)
-----
UPDATE WorkHours
SET Task = 'upadate taska'
WHERE WorkHoursId = 2
-----
DELETE WorkHours
WHERE WorkHoursId = 2
-- TEST TRIGGER

DROP TRIGGER WorkHourTrigger
--Problem 31.	Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
--
--
--Problem 32.	Start a database transaction, delete all employees from the 'Sales' department along with all dependent records from the pother tables. At the end rollback the transaction.
BEGIN TRAN
GO
ALTER TABLE Departments
DROP CONSTRAINT FK_Departments_Employees
GO
ALTER TABLE Employees
ADD CONSTRAINT FK_Departments_Employees FOREIGN KEY (DepartmentID)
	REFERENCES Departments (DepartmentID)
	ON DELETE CASCADE 
GO
DELETE FROM Employees 
WHERE DepartmentID IN 
	(
	SELECT DepartmentID 
	FROM Departments 
	WHERE Name = 'Sales'
)
ROLLBACK TRAN
--Problem 32.	Start a database transaction, delete all employees from the 'Sales' department along with all dependent records from the pother tables. At the end rollback the transaction.
--
--
--Problem 33.	Start a database transaction and drop the table EmployeesProjects.
BEGIN TRAN
DROP TABLE EmployeesProjects
ROLLBACK TRAN
--Problem 33.	Start a database transaction and drop the table EmployeesProjects.
--
--
--Problem 34.	Find how to use temporary tables in SQL Server.
--SELECT TOP 0 *
--INTO #tempTable
--FROM EmployeesProjects
--DROP TABLE EmployeesProjects
--Problem 34.	Find how to use temporary tables in SQL Server.

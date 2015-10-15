--------------------------------------------------------------------
--Problem 1.	Create a database with two tables
--------------------------------------------------------------------
CREATE TABLE Persons(
	Id int IDENTITY,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	SSN int,
	CONSTRAINT PK_PersonId PRIMARY KEY(Id)
)

CREATE TABLE Accounts(
	Id int IDENTITY,
	PersonId int NOT NULL,
	Balance float NOT NULL,
	CONSTRAINT PK_AccountId PRIMARY KEY(Id),
	CONSTRAINT FK_AccountId_PersonId FOREIGN KEY(PersonId) REFERENCES Persons
)

INSERT INTO Persons VALUES
('Pesho', 'OMF', 123),
('Plamen', 'Vasilev', 523),
('Ivan', 'Ganchev', 215),
('Nakov', 'Svetlin', 513)

INSERT INTO Accounts VALUES 
(1, 6111.5),
(2, 11.5),
(3, 1341.5),
(4, 3111.5)
-----
GO
ALTER PROC usp_FullNameOfAllPersons -- replace ALTER WITH CREATE
AS
SELECT FirstName + ' ' + LastName AS [Full name]
FROM Persons 
GO

EXEC usp_FullNameOfAllPersons
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 2.	Create a stored procedure
--------------------------------------------------------------------
GO
ALTER PROC usp_get_persons_balance_bigger_than(@money float = 0) -- replace ALTER WITH CREATE
AS
SELECT p.FirstName + ' ' + p.LastName AS [Full name], a.Balance
FROM Accounts a, Persons p 
WHERE a.PersonId = p.Id AND  a.Balance >= @money
GO

EXEC usp_get_persons_balance_bigger_than 2000
GO
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 3.	Create a function with parameters
--------------------------------------------------------------------
GO
ALTER FUNCTION ufn_CalculateYearlyInterestRate(@sum money , @rate float, @months int)  -- replace ALTER WITH CREATE
RETURNS money
AS
BEGIN
	DECLARE @Result money
	SET	@Result = ((@sum * (@rate / 100 + 1) ) * @months)
	RETURN @Result
END
GO

SELECT  dbo.ufn_CalculateYearlyInterestRate(1033,15,35) as YearlyIterestRate
GO
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 4.	Create a stored procedure that uses the function from the previous example.
--------------------------------------------------------------------
GO
ALTER PROC usp_get_interest_to_person(@AccountId int, @interestRate float) -- replace ALTER WITH CREATE
AS
SELECT p.FirstName + ' ' + p.LastName AS [Full name], dbo.ufn_CalculateYearlyInterestRate(a.Balanace, @interestRate, 1)
FROM Persons p, Accounts a
WHERE p.Id = a.PersonId AND a.Id = @AccountId

EXEC usp_get_interest_to_person 4, 1.2
GO
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 5.	Add two more stored procedures WithdrawMoney and DepositMoney.
--------------------------------------------------------------------
ALTER PROC usp_WithdrawMoney(@AccountId int, @money money) -- replace ALTER WITH CREATE
AS
BEGIN TRAN
IF (SELECT a.Balance FROM Accounts a WHERE a.PersonId = @AccountId) < @money
	BEGIN
		PRINT 'insufficient balance'
		ROLLBACK TRAN
	END
ELSE IF	@money <= 0
	BEGIN
		PRINT 'invalid balance'
		ROLLBACK TRAN
	END
ELSE 
	BEGIN
		UPDATE Accounts
		SET Balance = Balance - @money
		WHERE @AccountId = PersonId
		COMMIT TRAN
	END
GO

EXEC usp_WithdrawMoney 4, 1000
GO

------

ALTER PROC usp_DepositMoney (@AccountId int, @money money) -- replace ALTER WITH CREATE
AS
BEGIN TRAN
IF	@money <= 0
	BEGIN
		PRINT 'invalid balance'
		ROLLBACK TRAN
	END
ELSE
BEGIN
	UPDATE Accounts
	SET Balance = Balance + @money
	WHERE @AccountId = PersonId
	COMMIT TRAN
END
GO

EXEC usp_DepositMoney 4, 999
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 6.	Create table Logs.
--------------------------------------------------------------------
GO
CREATE TABLE Logs (
	LogId int IDENTITY,
	AccountId int NOT NULL,
	OldSum money NOT NULL,
	NewSum money NOT NULL,
	CONSTRAINT PK_LogId PRIMARY KEY(LogId),
	CONSTRAINT FK_LogId_AccountID FOREIGN KEY(AccountId) REFERENCES Accounts
)
-----
GO
CREATE TRIGGER SumChangeTrigger
ON Accounts
FOR UPDATE, DELETE
AS
INSERT Logs(AccountId, OldSum, NewSum)
SELECT d.PersonId, d.Balance, i.Balance
FROM deleted d, inserted i

EXEC usp_DepositMoney 2, 55
GO
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 7.	Define function in the SoftUni database.
--------------------------------------------------------------------
USE SoftUni
GO
ALTER FUNCTION ufn_IsComprised (@set nvarchar(MAX), @name nvarchar(MAX))  -- replace ALTER WITH CREATE
RETURNS INT
AS
BEGIN
	DECLARE @i int = 1
	WHILE( @i <= LEN(@name))
	BEGIN
		IF CHARINDEX(SUBSTRING(@name, @i, 1), @set) = 0
		BEGIN
			RETURN 0
		END
		SET @i = @i + 1
	END
	RETURN 1
END
GO

USE SoftUni
SELECT FirstName as [Names]
FROM Employees e
WHERE dbo.ufn_IsComprised('oistmiahf', e.FirstName) = 1 
-----
SELECT FirstName as [Names]
FROM Employees e
WHERE dbo.ufn_IsComprised('oistmiahf', e.MiddleName) = 1 
-----
SELECT FirstName as [Names]
FROM Employees e
WHERE dbo.ufn_IsComprised('oistmiahf', e.LastName) = 1 
-----
SELECT t.Name
FROM Towns t
WHERE dbo.ufn_IsComprised('oistmiahf', t.Name) = 1
GO
--------------------------------------------------------------------


--------------------------------------------------------------------
--Problem 8.	Using database cursor write a T-SQL
--------------------------------------------------------------------
-- TODO
--------------------------------------------------------------------



--------------------------------------------------------------------
--Problem 9.	Define a .NET aggregate function
--------------------------------------------------------------------
USE SoftUni
GO

ALTER FUNCTION fn_StrConcat(@input NVARCHAR(MAX)) -- replace ALTER WITH CREATE
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @i INT = 1,
			@currentChar NVARCHAR(1) = ' ',
			@result NVARCHAR(MAX) = ''
	WHILE (@i <= LEN(@input))
	BEGIN
		SET @currentChar = SUBSTRING(@input, @i, 1)
		IF @currentChar = ' ' 
		BEGIN
			IF @i <> 1 AND @i <> LEN(@input)
			BEGIN
				SET @currentChar = ','
			END
		END
		SET @result = @result +	@currentChar
		SET @i += 1
	END
	RETURN @result
END
GO

SELECT dbo.fn_StrConcat(FirstName + ' ' + LastName) AS [Name]
FROM Employees
GO
--------------------------------------------------------------------

--------------------------------------------------------------------
--Problem 10.	*Write a T-SQL script
--------------------------------------------------------------------

--------------------------------------------------------------------

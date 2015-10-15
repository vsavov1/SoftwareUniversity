-------------------------------------------------------------------------
--Problem 1.	Create a table in SQL Server
-------------------------------------------------------------------------
CREATE TABLE TestTable (
	Id INT IDENTITY,
	Date DATE,
	Text NVARCHAR(50),
	CONSTRAINT PK_ID PRIMARY KEY(Id)
)

DECLARE @Counter int = 0
WHILE @Counter < 10000000
BEGIN
DECLARE @Text nvarchar(100) = 
    'Text ' + CONVERT(nvarchar(100), @Counter) + ': ' +
    CONVERT(nvarchar(100), newid())
 DECLARE @Date datetime = 
	DATEADD(month, CONVERT(varbinary, newid()) % (50 * 12), getdate())
  INSERT INTO TestTable(Date, Text)
  SELECT @Date, @Text
  SET @Counter = @Counter + 1
END

SELECT COUNT(*) FROM TestTable

CHECKPOINT; DBCC DROPCLEANBUFFERS;
SELECT Date 
FROM TestTable
WHERE Date BETWEEN '2000-01-01' AND '2005-05-05'
--Without cache ~ 7 second
--With chache ~ 5 second
-------------------------------------------------------------------------


-------------------------------------------------------------------------
--Problem 2.	Add an index to speed-up the search by date 
-------------------------------------------------------------------------
CHECKPOINT; DBCC DROPCLEANBUFFERS;

CREATE INDEX DateIndex
ON TestTable(Date)

SELECT Date 
FROM TestTable
WHERE Date BETWEEN '2000-01-01' AND '2005-05-05'
--Without cache ~ 5 second
--With chache ~ 5 second
-------------------------------------------------------------------------



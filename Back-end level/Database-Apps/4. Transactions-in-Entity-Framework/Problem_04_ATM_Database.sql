USE master
GO

CREATE DATABASE ATMDatebase
GO

USE SoftUni
GO

CREATE TABLE CardAccounts(
    Id int IDENTITY NOT NULL,
	CardNumber nchar(10) NOT NULL,
	CardPIN nchar(4) NOT NULL,
	CardCash money NOT NULL,
  CONSTRAINT PK_UserCardAccounts PRIMARY KEY CLUSTERED(Id ASC)
)
GO

USE SoftUni
INSERT INTO CardAccounts (CardNumber,CardPIN,CardCash)
VALUES
(1234567890, 1234, 1234),
(3482185284, 3157, 1000),
(9384771738, 3214, 2000)

GO


USE ATMDatebase
GO
CREATE TABLE TransactionsHistory(
    Id int IDENTITY NOT NULL,
	CardNumber nchar(10) NOT NULL,
	Date DateTime NOT NULL,
	Amount money NOT NULL,
  CONSTRAINT PK_TransactionsHistory PRIMARY KEY CLUSTERED(Id ASC)
)
GO
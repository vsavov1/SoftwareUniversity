-- phpMyAdmin SQL Dump
-- version 4.0.9
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Време на генериране: 
-- Версия на сървъра: 5.6.14
-- Версия на PHP: 5.5.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- БД: `dbtest32`
--

DELIMITER $$
--
-- Процедури
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `fillWithDatea`(count INT)
BEGIN
set @i = 0;
  WHILE @i < count do

INSERT INTO `TestTablePerformances` 
VALUES (
(SELECT timestamp('1975-01-01 00:00:01') - INTERVAL FLOOR( RAND( ) * 36650) DAY),
'dsadasdasdsad'
);

set @ind = @i + 1;
END WHILE;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Структура на таблица `testtableperformances`
--

CREATE TABLE IF NOT EXISTS `testtableperformances` (
  `sampleDate` datetime DEFAULT NULL,
  `sampleText` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8
/*!50100 PARTITION BY RANGE (Year(sampleDate))
(PARTITION p0 VALUES LESS THAN (1980) ENGINE = InnoDB,
 PARTITION p1 VALUES LESS THAN (2000) ENGINE = InnoDB,
 PARTITION p2 VALUES LESS THAN (2020) ENGINE = InnoDB) */;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

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
-- БД: `university`
--

-- --------------------------------------------------------

--
-- Структура на таблица `courses`
--

CREATE TABLE IF NOT EXISTS `courses` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) COLLATE utf16_unicode_ci NOT NULL,
  `Student` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_unicode_ci;

-- --------------------------------------------------------

--
-- Структура на таблица `deparmetns`
--

CREATE TABLE IF NOT EXISTS `deparmetns` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) COLLATE utf16_unicode_ci NOT NULL,
  `Professors` int(11) NOT NULL,
  `Courses` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_unicode_ci;

-- --------------------------------------------------------

--
-- Структура на таблица `faculties`
--

CREATE TABLE IF NOT EXISTS `faculties` (
  `Id` int(11) NOT NULL,
  `Deparments` int(11) NOT NULL,
  `Name` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_unicode_ci;

-- --------------------------------------------------------

--
-- Структура на таблица `professors`
--

CREATE TABLE IF NOT EXISTS `professors` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) COLLATE utf16_unicode_ci NOT NULL,
  `Titles` varchar(50) COLLATE utf16_unicode_ci NOT NULL,
  `Courses` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_unicode_ci;

-- --------------------------------------------------------

--
-- Структура на таблица `students`
--

CREATE TABLE IF NOT EXISTS `students` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) COLLATE utf16_unicode_ci NOT NULL,
  `Courses` int(11) NOT NULL,
  `Faculty` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf16 COLLATE=utf16_unicode_ci;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

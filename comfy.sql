-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 25, 2023 at 06:10 PM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `comfy`
--

-- --------------------------------------------------------

--
-- Table structure for table `addresses`
--

CREATE TABLE `addresses` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `Country` varchar(50) NOT NULL,
  `City` varchar(50) NOT NULL,
  `Street` varchar(50) NOT NULL,
  `Building` longtext NOT NULL,
  `ApartmentsNumber` int(11) NOT NULL,
  `PostalCode` int(11) NOT NULL,
  `AddressTypeId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `addresstypes`
--

CREATE TABLE `addresstypes` (
  `Id` int(11) NOT NULL,
  `Type` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `answers`
--

CREATE TABLE `answers` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `Text` longtext NOT NULL,
  `UsefullAnswerCount` int(11) NOT NULL,
  `NeedlesslyAnswerCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `TargetId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroleclaims`
--

CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL,
  `RoleId` char(36) CHARACTER SET ascii NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetroles`
--

CREATE TABLE `aspnetroles` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserclaims`
--

CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `aspnetuserclaims`
--

INSERT INTO `aspnetuserclaims` (`Id`, `UserId`, `ClaimType`, `ClaimValue`) VALUES
(1, '08db0064-5aa1-43aa-8082-f7869dddf20e', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Manager'),
(10, '08db0551-5f33-4c69-8e48-689074a90b2b', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Owner'),
(11, '08db093b-7f4d-47a7-8a8e-100e9ac3ff9c', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'SeniorManager'),
(12, '08db093b-93f2-4610-8d56-14cccccc04c8', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Administrator'),
(14, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Administrator'),
(15, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Administrator'),
(16, '08db0a16-be64-48d5-8167-24c990fd892c', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Administrator'),
(17, '08db0bad-4fa2-45be-8245-9d67230ae3ab', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Administrator'),
(18, '08db0c2b-2c36-427e-8430-1d02b4d41806', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'SeniorManager');

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserlogins`
--

CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetuserroles`
--

CREATE TABLE `aspnetuserroles` (
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `RoleId` char(36) CHARACTER SET ascii NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusers`
--

CREATE TABLE `aspnetusers` (
  `Id` char(36) CHARACTER SET ascii NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `aspnetusers`
--

INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('08db0064-5aa1-43aa-8082-f7869dddf20e', 'manager', 'MANAGER', 'email@gmail.com', 'EMAIL@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEC7Eo7nw1dmGou/cGWo/5y1BSXWrkrDHrciwYt/BzU6z0IvrTGGZOG0Ijki9MxbS3w==', 'LLV6PL2CMNVCS2WFGDRVM7Z76XBNN3KO', 'e1cdb862-2d1e-4671-85e5-e3a7f5249639', '88005553535', 0, 0, NULL, 1, 0),
('08db0551-5f33-4c69-8e48-689074a90b2b', 'owner', 'OWNER', 'owner@gmail.com', 'OWNER@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEKX7Vt/FGMDGkFAEDuDD6qeW+N1pR5mDpxL1+5mPn6J8uPpFVSSOuZbKb9WGVajr7w==', 'HJPDGWIJK3C73PWBGRJNCWLH7LGVGWL7', '3e81562c-927e-4725-acf8-5ee682fdd967', '88005553535', 0, 0, NULL, 1, 0),
('08db093b-7f4d-47a7-8a8e-100e9ac3ff9c', 'smanager', 'SMANAGER', 'smanager@mail', 'SMANAGER@MAIL', 0, 'AQAAAAEAACcQAAAAELu5lWpM16TrlOLIxzfqD2Zl8H9V28kpGTlrTRdvzeZiqnxtxAobM2e8pu3wnuvTmw==', '3D3HKR54BM52DAK2HVB6PYHFMYD3PWL2', '0507974e-b343-4718-95dd-0fa0da00fe7f', '274628746', 0, 0, NULL, 1, 0),
('08db093b-93f2-4610-8d56-14cccccc04c8', 'admin', 'ADMIN', 'admin@email.com', 'ADMIN@EMAIL.COM', 0, 'AQAAAAEAACcQAAAAELHX4s5CLRSKmVgZUuzzY/sYlycq/dCh54/GXJ6SOeyMup8ioWDZiN7h0tKp5igeiA==', 'SAYYOYGHFKOB2YBWX3BUSUDDYGKNHX6Q', '1a0e1c52-bc34-4a25-b99c-85558fd5b045', '12344215464', 0, 0, '2999-02-28 22:00:00.000000', 1, 0),
('08db0a15-6a72-4ba6-8ab5-3ce352cefee9', 'manager1', 'MANAGER1', 'manager@gmail.com', 'MANAGER@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEBAIdUqhgy7W7CTZf7p/PY2ZfPYcmxfYrj15dZang7MxPryZqRuwCJG+HnAY3sWfgg==', '44AUKDYIKRJMMPHMOIUUCHKG3RST6RW4', 'aaaf4fad-4f6a-4049-8c81-3587f5796565', '88005553535', 0, 0, '2999-02-28 22:00:00.000000', 1, 0),
('08db0a15-cdfb-41b3-87fa-b7c65148a7d9', 'manager2', 'MANAGER2', 'manager@gmail.com', 'MANAGER@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAELcOwWMx/FBNNH8uel22VmyLNw4aeDMy9cIpLpPy2G7TwcOQKQsyWcVpYGenHri7Sg==', '3CFADOUT727QHN4DD5CUG7BAKIIHQERT', '7f8de556-2ce2-4f58-ac16-31341f69dbbc', '88005553535', 0, 0, '2999-02-28 22:00:00.000000', 1, 0),
('08db0a16-be64-48d5-8167-24c990fd892c', 'testadmin', 'TESTADMIN', 'testadmin@gmail.com', 'TESTADMIN@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEC2IGk3Av3Rn4W6hwv36vseUXXEVjz7gUQXwdgGX1RLOri0ucJY+7c/sbemwcEJVVw==', 'GDHH3RODL4I5VZNXFZVCEYLNL6L2AR4K', '6cc7b91f-f83d-4cd9-aacc-1123bc032a28', '13212312', 0, 0, '2999-02-28 22:00:00.000000', 1, 0),
('08db0bad-4fa2-45be-8245-9d67230ae3ab', 'TESTING', 'TESTING', 'manager@gmail.com', 'MANAGER@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEIaGRZEP0cXE05Gl9KT0Stxd3m8cg/VhCYWE/8XecQ8Y2gQy6bEzTQLI1Mj0qbqazA==', 'YR4ZWKUHV5SIMCM5VZQMNWMGEJOOE3AS', '992c68ea-cf5c-48ff-924b-26591d35413d', '88005553535', 0, 0, '2999-02-28 22:00:00.000000', 1, 0),
('08db0c2b-2c36-427e-8430-1d02b4d41806', 'testing11', 'TESTING11', 'testing1102@gmail.com', 'TESTING1102@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEDp41FEGX74oS4RF00IaNmk/xQCbqdGq87BMIoTmXabYt5qPOi3HwtYo+p9NVZmxUA==', '67CF3ENVSPQCORMFRUE2FVSMMT4ZW6R5', '7eee285d-d11f-4748-abd0-021f878ff392', '8800555353534343', 0, 0, '2999-02-28 22:00:00.000000', 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `aspnetusertokens`
--

CREATE TABLE `aspnetusertokens` (
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `brands`
--

CREATE TABLE `brands` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `brands`
--

INSERT INTO `brands` (`Id`, `Name`) VALUES
(1, 'test_brand1'),
(2, 'test_brand2'),
(3, 'test_brand3'),
(4, 'test_brand4'),
(5, 'test_brand5'),
(6, 'test_brand6'),
(7, 'test_brand7'),
(8, 'test_brand8'),
(9, 'brand1'),
(10, 'brand2'),
(11, 'brand3'),
(12, 'brand4');

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`Id`, `Name`) VALUES
(1, 'category1'),
(2, 'category1'),
(3, 'category2'),
(4, 'category2'),
(5, 'category4'),
(6, 'category5'),
(7, 'category6'),
(8, 'category_test1'),
(9, 'category_test2'),
(10, 'category_test3');

-- --------------------------------------------------------

--
-- Table structure for table `characteristics`
--

CREATE TABLE `characteristics` (
  `Id` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `CharacteristicsNameId` int(11) NOT NULL,
  `CharacteristicsValueId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `characteristics`
--

INSERT INTO `characteristics` (`Id`, `ProductId`, `CharacteristicsNameId`, `CharacteristicsValueId`) VALUES
(4, 21, 1, 1),
(5, 21, 2, 2),
(7, 20, 1, 5),
(8, 21, 3, 3);

-- --------------------------------------------------------

--
-- Table structure for table `characteristicsnames`
--

CREATE TABLE `characteristicsnames` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `characteristicsnames`
--

INSERT INTO `characteristicsnames` (`Id`, `Name`) VALUES
(1, 'color'),
(2, 'size'),
(3, 'weight'),
(4, 'sizex'),
(5, 'test'),
(6, 'test2');

-- --------------------------------------------------------

--
-- Table structure for table `characteristicsvalues`
--

CREATE TABLE `characteristicsvalues` (
  `Id` int(11) NOT NULL,
  `Value` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `characteristicsvalues`
--

INSERT INTO `characteristicsvalues` (`Id`, `Value`) VALUES
(1, 'red'),
(2, 'big'),
(3, '30'),
(4, 'blue'),
(5, 'black'),
(6, 'large'),
(7, 'test2'),
(8, 'test');

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

CREATE TABLE `images` (
  `Id` int(11) NOT NULL,
  `Url` longtext NOT NULL,
  `ProductId` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loggingactions`
--

CREATE TABLE `loggingactions` (
  `Id` int(11) NOT NULL,
  `Action` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `loggingactions`
--

INSERT INTO `loggingactions` (`Id`, `Action`) VALUES
(1, 'Create'),
(2, 'Update'),
(3, 'Lockout'),
(4, 'Activate');

-- --------------------------------------------------------

--
-- Table structure for table `models`
--

CREATE TABLE `models` (
  `Id` int(11) NOT NULL,
  `Model` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `models`
--

INSERT INTO `models` (`Id`, `Model`) VALUES
(1, 'model1'),
(2, 'model2'),
(3, 'model3');

-- --------------------------------------------------------

--
-- Table structure for table `orderedproducts`
--

CREATE TABLE `orderedproducts` (
  `Id` int(11) NOT NULL,
  `OrderId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `Id` int(11) NOT NULL,
  `Description` varchar(50) NOT NULL,
  `TotalSum` int(11) NOT NULL,
  `CreatingDate` date NOT NULL,
  `ReceivingDate` date NOT NULL,
  `ProductCount` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `AddressId` int(11) NOT NULL,
  `PaymentTypeId` int(11) NOT NULL,
  `StatusId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `orderstatuses`
--

CREATE TABLE `orderstatuses` (
  `Id` int(11) NOT NULL,
  `Status` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `paymenttypes`
--

CREATE TABLE `paymenttypes` (
  `Id` int(11) NOT NULL,
  `Type` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `pricehistory`
--

CREATE TABLE `pricehistory` (
  `Id` int(11) NOT NULL,
  `Price` int(11) NOT NULL,
  `Date` date NOT NULL,
  `ProductId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `pricehistory`
--

INSERT INTO `pricehistory` (`Id`, `Price`, `Date`, `ProductId`) VALUES
(1, 228, '2023-02-23', 19),
(2, 1337, '2023-02-23', 19),
(3, 569, '2023-02-23', 19),
(4, 57999, '2023-02-23', 20),
(5, 58, '2023-02-23', 21),
(6, 60, '2023-02-23', 21),
(7, 61, '2023-02-23', 21),
(8, 100, '2023-02-25', 21);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `Id` int(11) NOT NULL,
  `Price` int(11) NOT NULL,
  `DiscountAmmount` int(11) NOT NULL,
  `Amount` int(11) NOT NULL,
  `Code` int(11) NOT NULL,
  `Rating` double NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `BrandId` int(11) NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `ModelId` int(11) NOT NULL,
  `Description` longtext DEFAULT NULL,
  `Name` varchar(255) NOT NULL,
  `Url` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`Id`, `Price`, `DiscountAmmount`, `Amount`, `Code`, `Rating`, `IsActive`, `BrandId`, `CategoryId`, `ModelId`, `Description`, `Name`, `Url`) VALUES
(17, 23, 0, 0, 1000017, 0, 0, 9, 1, 1, 'yellow banana', 'banana', 'banana-1000017'),
(19, 569, 0, 0, 1000019, 0, 0, 9, 1, 1, 'Test Description', 'Test product4', 'test-product4-1000019'),
(20, 57999, 0, 0, 1000020, 0, 0, 9, 1, 1, 'Смартфон Samsung Galaxy S23 Ultra 12/256Gb Green', 'Смартфон Samsung Galaxy S23 Ultra 12/256Gb Green (SM-S918BZGGSEK)', 'smartfon-samsung-galaxy-s23-ultra-12-256gb-green-sm-s918bzggsek-1000020'),
(21, 100, 0, 0, 1000021, 0, 1, 9, 1, 1, 'обычное яблоко', 'красное яблоко в хлебопечке', 'krasnoe-yabloko-v-xlebopechke-1000021');

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `Id` int(11) NOT NULL,
  `Text` longtext NOT NULL,
  `CreateDate` date NOT NULL,
  `UsefullQuestionCount` int(11) NOT NULL,
  `NeedlesslyCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UserId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `Text` longtext NOT NULL,
  `Advantages` varchar(50) NOT NULL,
  `Disadvantages` varchar(50) NOT NULL,
  `CreateDate` date NOT NULL,
  `ProductRating` double NOT NULL,
  `UsefullReviewCount` int(11) NOT NULL,
  `NeedlesslyReviewCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `ProductId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `userlogs`
--

CREATE TABLE `userlogs` (
  `Id` int(11) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `LoggingActionId` int(11) NOT NULL,
  `SubjectUserId` char(36) CHARACTER SET ascii NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `userlogs`
--

INSERT INTO `userlogs` (`Id`, `UserId`, `LoggingActionId`, `SubjectUserId`) VALUES
(1, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0551-5f33-4c69-8e48-689074a90b2b'),
(2, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-7f4d-47a7-8a8e-100e9ac3ff9c'),
(3, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-7f4d-47a7-8a8e-100e9ac3ff9c'),
(4, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-7f4d-47a7-8a8e-100e9ac3ff9c'),
(8, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(9, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(10, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(11, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(12, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(13, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(14, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(15, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(16, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(17, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(18, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(19, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(20, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(21, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(22, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(23, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(24, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(25, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(26, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(27, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(28, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a16-be64-48d5-8167-24c990fd892c'),
(29, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(30, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0a16-be64-48d5-8167-24c990fd892c'),
(31, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(32, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(33, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0a16-be64-48d5-8167-24c990fd892c'),
(34, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0a15-cdfb-41b3-87fa-b7c65148a7d9'),
(35, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0a15-6a72-4ba6-8ab5-3ce352cefee9'),
(36, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(37, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(38, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0a16-be64-48d5-8167-24c990fd892c'),
(39, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0a16-be64-48d5-8167-24c990fd892c'),
(40, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(41, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(42, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(43, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(44, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(45, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(46, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(47, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(48, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(49, '08db0551-5f33-4c69-8e48-689074a90b2b', 1, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(50, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(51, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(52, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(53, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(54, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(55, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(56, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(57, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(58, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db0bad-4fa2-45be-8245-9d67230ae3ab'),
(59, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(60, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(61, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(62, '08db0064-5aa1-43aa-8082-f7869dddf20e', 4, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(63, '08db0064-5aa1-43aa-8082-f7869dddf20e', 3, '08db0c2b-2c36-427e-8430-1d02b4d41806'),
(64, '08db0551-5f33-4c69-8e48-689074a90b2b', 2, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(65, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(66, '08db0551-5f33-4c69-8e48-689074a90b2b', 4, '08db093b-93f2-4610-8d56-14cccccc04c8'),
(67, '08db0551-5f33-4c69-8e48-689074a90b2b', 3, '08db093b-93f2-4610-8d56-14cccccc04c8');

-- --------------------------------------------------------

--
-- Table structure for table `whishlists`
--

CREATE TABLE `whishlists` (
  `Id` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20230126090624_Initial', '6.0.13'),
('20230127123824_Initial', '6.0.13'),
('20230208182846_UserLogs', '6.0.13'),
('20230208200712_UserLogsv2', '6.0.13'),
('20230208204237_UserIsDeleted', '6.0.13'),
('20230208205827_deletedUserIsDeleted', '6.0.13'),
('20230217173220_MainTables', '6.0.13'),
('20230220185250_ProductUpdate', '6.0.13'),
('20230220185452_ProductUpdate2', '6.0.13'),
('20230220195443_ProductUpdate3', '6.0.13'),
('20230220200554_ProductUpdate4', '6.0.13'),
('20230222171253_ProductUpdate5', '6.0.13'),
('20230222173425_ProductUpdate6', '6.0.13'),
('20230222173909_ProductUpdate7', '6.0.13'),
('20230222204535_onetmanyrelationship1', '6.0.14'),
('20230223131237_ProductUpdate8', '6.0.14'),
('20230223172459_ProductUpdate9', '6.0.14'),
('20230225105829_FixForCharacteristicsId', '6.0.14');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `addresses`
--
ALTER TABLE `addresses`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Addresses_AddressTypeId` (`AddressTypeId`);

--
-- Indexes for table `addresstypes`
--
ALTER TABLE `addresstypes`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `answers`
--
ALTER TABLE `answers`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Answers_TargetId` (`TargetId`);

--
-- Indexes for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Indexes for table `aspnetroles`
--
ALTER TABLE `aspnetroles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indexes for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Indexes for table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Indexes for table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Indexes for table `aspnetusers`
--
ALTER TABLE `aspnetusers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indexes for table `aspnetusertokens`
--
ALTER TABLE `aspnetusertokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indexes for table `brands`
--
ALTER TABLE `brands`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `characteristics`
--
ALTER TABLE `characteristics`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Characteristics_CharacteristicsNameId` (`CharacteristicsNameId`),
  ADD KEY `IX_Characteristics_CharacteristicsValueId` (`CharacteristicsValueId`),
  ADD KEY `IX_Characteristics_ProductId` (`ProductId`);

--
-- Indexes for table `characteristicsnames`
--
ALTER TABLE `characteristicsnames`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `characteristicsvalues`
--
ALTER TABLE `characteristicsvalues`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `images`
--
ALTER TABLE `images`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Images_ProductId` (`ProductId`);

--
-- Indexes for table `loggingactions`
--
ALTER TABLE `loggingactions`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UC_LoggingActions_Action` (`Action`) USING HASH;

--
-- Indexes for table `models`
--
ALTER TABLE `models`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `orderedproducts`
--
ALTER TABLE `orderedproducts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_OrderedProducts_OrderId` (`OrderId`),
  ADD KEY `IX_OrderedProducts_ProductId` (`ProductId`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Orders_AddressId` (`AddressId`),
  ADD KEY `IX_Orders_PaymentTypeId` (`PaymentTypeId`),
  ADD KEY `IX_Orders_StatusId` (`StatusId`);

--
-- Indexes for table `orderstatuses`
--
ALTER TABLE `orderstatuses`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `paymenttypes`
--
ALTER TABLE `paymenttypes`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `pricehistory`
--
ALTER TABLE `pricehistory`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_PriceHistory_ProductId` (`ProductId`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Products_Code` (`Code`),
  ADD UNIQUE KEY `IX_Products_Url` (`Url`),
  ADD KEY `IX_Products_BrandId` (`BrandId`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`),
  ADD KEY `IX_Products_ModelId` (`ModelId`),
  ADD KEY `IX_Products_Name` (`Name`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Questions_ProductId` (`ProductId`);

--
-- Indexes for table `reviews`
--
ALTER TABLE `reviews`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reviews_ProductId` (`ProductId`);

--
-- Indexes for table `userlogs`
--
ALTER TABLE `userlogs`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_UserLogs_LoggingActionId` (`LoggingActionId`),
  ADD KEY `IX_UserLogs_SubjectUserId` (`SubjectUserId`),
  ADD KEY `IX_UserLogs_UserId` (`UserId`);

--
-- Indexes for table `whishlists`
--
ALTER TABLE `whishlists`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_WhishLists_ProductId` (`ProductId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `addresses`
--
ALTER TABLE `addresses`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `addresstypes`
--
ALTER TABLE `addresstypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `answers`
--
ALTER TABLE `answers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `brands`
--
ALTER TABLE `brands`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `characteristics`
--
ALTER TABLE `characteristics`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `characteristicsnames`
--
ALTER TABLE `characteristicsnames`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `characteristicsvalues`
--
ALTER TABLE `characteristicsvalues`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `images`
--
ALTER TABLE `images`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `loggingactions`
--
ALTER TABLE `loggingactions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `models`
--
ALTER TABLE `models`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `orderedproducts`
--
ALTER TABLE `orderedproducts`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `orderstatuses`
--
ALTER TABLE `orderstatuses`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `paymenttypes`
--
ALTER TABLE `paymenttypes`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pricehistory`
--
ALTER TABLE `pricehistory`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `reviews`
--
ALTER TABLE `reviews`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `userlogs`
--
ALTER TABLE `userlogs`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=68;

--
-- AUTO_INCREMENT for table `whishlists`
--
ALTER TABLE `whishlists`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `addresses`
--
ALTER TABLE `addresses`
  ADD CONSTRAINT `FK_Addresses_AddressTypes` FOREIGN KEY (`AddressTypeId`) REFERENCES `addresstypes` (`Id`);

--
-- Constraints for table `answers`
--
ALTER TABLE `answers`
  ADD CONSTRAINT `FK_Answers_Questions` FOREIGN KEY (`TargetId`) REFERENCES `questions` (`Id`),
  ADD CONSTRAINT `FK_Answers_Reviews` FOREIGN KEY (`TargetId`) REFERENCES `reviews` (`Id`);

--
-- Constraints for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserlogins`
--
ALTER TABLE `aspnetuserlogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetuserroles`
--
ALTER TABLE `aspnetuserroles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `aspnetusertokens`
--
ALTER TABLE `aspnetusertokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `characteristics`
--
ALTER TABLE `characteristics`
  ADD CONSTRAINT `FK_Characteristics_CharacteristicsNames_CharacteristicsNameId` FOREIGN KEY (`CharacteristicsNameId`) REFERENCES `characteristicsnames` (`Id`),
  ADD CONSTRAINT `FK_Characteristics_CharacteristicsValues_CharacteristicsValueId` FOREIGN KEY (`CharacteristicsValueId`) REFERENCES `characteristicsvalues` (`Id`),
  ADD CONSTRAINT `FK_Characteristics_Products` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `images`
--
ALTER TABLE `images`
  ADD CONSTRAINT `FK_Images_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `orderedproducts`
--
ALTER TABLE `orderedproducts`
  ADD CONSTRAINT `FK_OrderedProducts_Orders` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`),
  ADD CONSTRAINT `FK_OrderedProducts_Products` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `FK_Orders_Addresses` FOREIGN KEY (`AddressId`) REFERENCES `addresses` (`Id`),
  ADD CONSTRAINT `FK_Orders_OrderStatuses` FOREIGN KEY (`StatusId`) REFERENCES `orderstatuses` (`Id`),
  ADD CONSTRAINT `FK_Orders_PaymentTypes` FOREIGN KEY (`PaymentTypeId`) REFERENCES `paymenttypes` (`Id`);

--
-- Constraints for table `pricehistory`
--
ALTER TABLE `pricehistory`
  ADD CONSTRAINT `FK_PriceHistory_Products` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `FK_Products_Brands` FOREIGN KEY (`BrandId`) REFERENCES `brands` (`Id`),
  ADD CONSTRAINT `FK_Products_Categories` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`Id`),
  ADD CONSTRAINT `FK_Products_Models` FOREIGN KEY (`ModelId`) REFERENCES `models` (`Id`);

--
-- Constraints for table `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `FK_Questions_Products` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `reviews`
--
ALTER TABLE `reviews`
  ADD CONSTRAINT `FK_Reviews_Products` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `userlogs`
--
ALTER TABLE `userlogs`
  ADD CONSTRAINT `FK_UserLogs_AspNetUsers_SubjectUserId` FOREIGN KEY (`SubjectUserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UserLogs_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UserLogs_LoggingActions_LoggingActionId` FOREIGN KEY (`LoggingActionId`) REFERENCES `loggingactions` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `whishlists`
--
ALTER TABLE `whishlists`
  ADD CONSTRAINT `FK_WhishLists_Products` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

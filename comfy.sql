-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 23, 2023 at 08:06 PM
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
  `Country` longtext NOT NULL,
  `City` longtext NOT NULL,
  `Street` longtext NOT NULL,
  `Building` longtext NOT NULL,
  `ApartmentsNumber` int(11) NOT NULL,
  `PostalCode` int(11) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `AddressTypeId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `addresstypes`
--

CREATE TABLE `addresstypes` (
  `Id` int(11) NOT NULL,
  `Type` longtext NOT NULL
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
(1, '08db2bc4-64b1-4307-8371-9b3f832e3c15', 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', 'Owner');

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
('08db2bc4-64b1-4307-8371-9b3f832e3c15', 'owner', 'OWNER', NULL, NULL, 0, 'AQAAAAEAACcQAAAAEOleSRrPWNv0NVHe8vu+45FRd2nRPe+9OcWa4cGE7dTrx78moEeY9AJILGkk3+xplg==', 'UPFU6NHIYDZCNRCAM2VBMOEO4JGVMT7J', 'c2f40989-d71f-4330-9267-93ca8733e256', NULL, 0, 0, NULL, 1, 0);

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
  `Name` longtext NOT NULL,
  `SubcategoryId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `brands`
--

INSERT INTO `brands` (`Id`, `Name`, `SubcategoryId`) VALUES
(1, 'Apple', 1),
(2, 'Lenovo', 3);

-- --------------------------------------------------------

--
-- Table structure for table `characteristics`
--

CREATE TABLE `characteristics` (
  `Id` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `CharacteristicsNameId` int(11) NOT NULL,
  `CharacteristicsValueId` int(11) NOT NULL,
  `SubcategoryId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `characteristicsnames`
--

CREATE TABLE `characteristicsnames` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `characteristicsvalues`
--

CREATE TABLE `characteristicsvalues` (
  `Id` int(11) NOT NULL,
  `Value` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

CREATE TABLE `images` (
  `Id` int(11) NOT NULL,
  `Url` longtext NOT NULL,
  `ProductId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `loggingactions`
--

CREATE TABLE `loggingactions` (
  `Id` int(11) NOT NULL,
  `Action` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `maincategories`
--

CREATE TABLE `maincategories` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `ImageId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `maincategories`
--

INSERT INTO `maincategories` (`Id`, `Name`, `ImageId`) VALUES
(1, 'Смартфони та телефони', NULL),
(2, 'Ноутбуки, планшети та комп\'ютерна техніка', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `maincategoryimages`
--

CREATE TABLE `maincategoryimages` (
  `Id` int(11) NOT NULL,
  `Url` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `models`
--

CREATE TABLE `models` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `models`
--

INSERT INTO `models` (`Id`, `Name`) VALUES
(1, 'iPhone 13'),
(2, 'IdeaPad Gaming 3');

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `Id` int(11) NOT NULL,
  `Description` longtext NOT NULL,
  `TotalSum` int(11) NOT NULL,
  `ReceivingDate` datetime(6) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `AddressId` int(11) NOT NULL,
  `PaymentTypeId` int(11) NOT NULL,
  `StatusId` int(11) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `orderstatuses`
--

CREATE TABLE `orderstatuses` (
  `Id` int(11) NOT NULL,
  `Status` longtext NOT NULL
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
-- Table structure for table `pricehistories`
--

CREATE TABLE `pricehistories` (
  `Id` int(11) NOT NULL,
  `Price` int(11) NOT NULL,
  `Date` datetime(6) NOT NULL,
  `ProductId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `pricehistories`
--

INSERT INTO `pricehistories` (`Id`, `Price`, `Date`, `ProductId`) VALUES
(1, 35499, '2023-03-23 19:39:57.458966', 1),
(3, 40499, '2023-03-23 19:41:44.771029', 3);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `Id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Description` longtext DEFAULT NULL,
  `Price` int(11) NOT NULL,
  `DiscountAmount` int(11) NOT NULL,
  `Amount` int(11) NOT NULL,
  `Code` int(11) NOT NULL,
  `Rating` double NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `Url` varchar(255) DEFAULT NULL,
  `BrandId` int(11) NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `ModelId` int(11) NOT NULL,
  `OrderId` int(11) DEFAULT NULL,
  `WishListId` int(11) DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`Id`, `Name`, `Description`, `Price`, `DiscountAmount`, `Amount`, `Code`, `Rating`, `IsActive`, `Url`, `BrandId`, `CategoryId`, `ModelId`, `OrderId`, `WishListId`, `CreatedAt`) VALUES
(1, 'Смартфон Apple iPhone 13 128Gb Starlight', ' Смартфон Apple iPhone 13 128Gb Starlight - классный телефон, ставлю класс', 35499, 0, 0, 1000001, 0, 0, 'smartfon-apple-iphone-13-128gb-starlight-1000001', 1, 1, 1, NULL, NULL, '2023-03-23 17:39:57.367300'),
(3, 'Смартфон Apple iPhone 13 256Gb Blue', ' Смартфон Apple iPhone 13 256Gb Blue - супер вау', 40499, 0, 0, 1000003, 0, 0, 'smartfon-apple-iphone-13-256gb-blue-1000003', 1, 1, 1, NULL, NULL, '2023-03-23 17:41:44.651365');

-- --------------------------------------------------------

--
-- Table structure for table `questionanswers`
--

CREATE TABLE `questionanswers` (
  `Id` int(11) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `Text` longtext NOT NULL,
  `UsefulAnswerCount` int(11) NOT NULL,
  `NeedlessAnswerCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `QuestionId` int(11) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `Id` int(11) NOT NULL,
  `Text` longtext NOT NULL,
  `UsefulQuestionCount` int(11) NOT NULL,
  `NeedlessQuestionCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `ProductId` int(11) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `reviewanswers`
--

CREATE TABLE `reviewanswers` (
  `Id` int(11) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `Text` longtext NOT NULL,
  `UsefulAnswerCount` int(11) NOT NULL,
  `NeedlessAnswerCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `ReviewId` int(11) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `Id` int(11) NOT NULL,
  `Text` longtext NOT NULL,
  `Advantages` longtext NOT NULL,
  `Disadvantages` longtext NOT NULL,
  `ProductRating` double NOT NULL,
  `UsefulReviewCount` int(11) NOT NULL,
  `NeedlessReviewCount` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL,
  `ProductId` int(11) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `subcategories`
--

CREATE TABLE `subcategories` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `MainCategoryId` int(11) NOT NULL,
  `ImageId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `subcategories`
--

INSERT INTO `subcategories` (`Id`, `Name`, `MainCategoryId`, `ImageId`) VALUES
(1, 'Смартфони', 1, NULL),
(2, 'Телефони', 1, NULL),
(3, 'Ноутбуки', 2, NULL),
(4, 'Планшети', 2, NULL),
(5, 'Системні блоки (ПК)', 2, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `subcategoryimages`
--

CREATE TABLE `subcategoryimages` (
  `Id` int(11) NOT NULL,
  `Url` longtext NOT NULL
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

-- --------------------------------------------------------

--
-- Table structure for table `wishlists`
--

CREATE TABLE `wishlists` (
  `Id` int(11) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii NOT NULL
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
('20230323173137_Initial', '6.0.15'),
('20230323190540_ProductCharacteristicObjectCycleErrorFix', '6.0.15');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `addresses`
--
ALTER TABLE `addresses`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Addresses_AddressTypeId` (`AddressTypeId`),
  ADD KEY `IX_Addresses_UserId` (`UserId`);

--
-- Indexes for table `addresstypes`
--
ALTER TABLE `addresstypes`
  ADD PRIMARY KEY (`Id`);

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
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Brands_SubcategoryId` (`SubcategoryId`);

--
-- Indexes for table `characteristics`
--
ALTER TABLE `characteristics`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Characteristics_CharacteristicsNameId` (`CharacteristicsNameId`),
  ADD KEY `IX_Characteristics_CharacteristicsValueId` (`CharacteristicsValueId`),
  ADD KEY `IX_Characteristics_ProductId` (`ProductId`),
  ADD KEY `IX_Characteristics_SubcategoryId` (`SubcategoryId`);

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
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `maincategories`
--
ALTER TABLE `maincategories`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_MainCategories_ImageId` (`ImageId`);

--
-- Indexes for table `maincategoryimages`
--
ALTER TABLE `maincategoryimages`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `models`
--
ALTER TABLE `models`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Orders_AddressId` (`AddressId`),
  ADD KEY `IX_Orders_PaymentTypeId` (`PaymentTypeId`),
  ADD KEY `IX_Orders_StatusId` (`StatusId`),
  ADD KEY `IX_Orders_UserId` (`UserId`);

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
-- Indexes for table `pricehistories`
--
ALTER TABLE `pricehistories`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_PriceHistories_ProductId` (`ProductId`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Products_Code` (`Code`),
  ADD UNIQUE KEY `IX_Products_Name` (`Name`),
  ADD UNIQUE KEY `IX_Products_Url` (`Url`),
  ADD KEY `IX_Products_BrandId` (`BrandId`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`),
  ADD KEY `IX_Products_ModelId` (`ModelId`),
  ADD KEY `IX_Products_OrderId` (`OrderId`),
  ADD KEY `IX_Products_WishListId` (`WishListId`);

--
-- Indexes for table `questionanswers`
--
ALTER TABLE `questionanswers`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_QuestionAnswers_QuestionId` (`QuestionId`),
  ADD KEY `IX_QuestionAnswers_UserId` (`UserId`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Questions_ProductId` (`ProductId`),
  ADD KEY `IX_Questions_UserId` (`UserId`);

--
-- Indexes for table `reviewanswers`
--
ALTER TABLE `reviewanswers`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_ReviewAnswers_ReviewId` (`ReviewId`),
  ADD KEY `IX_ReviewAnswers_UserId` (`UserId`);

--
-- Indexes for table `reviews`
--
ALTER TABLE `reviews`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reviews_ProductId` (`ProductId`),
  ADD KEY `IX_Reviews_UserId` (`UserId`);

--
-- Indexes for table `subcategories`
--
ALTER TABLE `subcategories`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Subcategories_ImageId` (`ImageId`),
  ADD KEY `IX_Subcategories_MainCategoryId` (`MainCategoryId`);

--
-- Indexes for table `subcategoryimages`
--
ALTER TABLE `subcategoryimages`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `userlogs`
--
ALTER TABLE `userlogs`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_UserLogs_LoggingActionId` (`LoggingActionId`),
  ADD KEY `IX_UserLogs_SubjectUserId` (`SubjectUserId`),
  ADD KEY `IX_UserLogs_UserId` (`UserId`);

--
-- Indexes for table `wishlists`
--
ALTER TABLE `wishlists`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_WishLists_UserId` (`UserId`);

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
-- AUTO_INCREMENT for table `aspnetroleclaims`
--
ALTER TABLE `aspnetroleclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `aspnetuserclaims`
--
ALTER TABLE `aspnetuserclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `brands`
--
ALTER TABLE `brands`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `characteristics`
--
ALTER TABLE `characteristics`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `characteristicsnames`
--
ALTER TABLE `characteristicsnames`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `characteristicsvalues`
--
ALTER TABLE `characteristicsvalues`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `images`
--
ALTER TABLE `images`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `loggingactions`
--
ALTER TABLE `loggingactions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `maincategories`
--
ALTER TABLE `maincategories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `maincategoryimages`
--
ALTER TABLE `maincategoryimages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `models`
--
ALTER TABLE `models`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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
-- AUTO_INCREMENT for table `pricehistories`
--
ALTER TABLE `pricehistories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `questionanswers`
--
ALTER TABLE `questionanswers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `reviewanswers`
--
ALTER TABLE `reviewanswers`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `reviews`
--
ALTER TABLE `reviews`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `subcategories`
--
ALTER TABLE `subcategories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `subcategoryimages`
--
ALTER TABLE `subcategoryimages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `userlogs`
--
ALTER TABLE `userlogs`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `wishlists`
--
ALTER TABLE `wishlists`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `addresses`
--
ALTER TABLE `addresses`
  ADD CONSTRAINT `FK_Addresses_AddressTypes_AddressTypeId` FOREIGN KEY (`AddressTypeId`) REFERENCES `addresstypes` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Addresses_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;

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
-- Constraints for table `brands`
--
ALTER TABLE `brands`
  ADD CONSTRAINT `FK_Brands_Subcategories_SubcategoryId` FOREIGN KEY (`SubcategoryId`) REFERENCES `subcategories` (`Id`);

--
-- Constraints for table `characteristics`
--
ALTER TABLE `characteristics`
  ADD CONSTRAINT `FK_Characteristics_CharacteristicsNames_CharacteristicsNameId` FOREIGN KEY (`CharacteristicsNameId`) REFERENCES `characteristicsnames` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Characteristics_CharacteristicsValues_CharacteristicsValueId` FOREIGN KEY (`CharacteristicsValueId`) REFERENCES `characteristicsvalues` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Characteristics_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Characteristics_Subcategories_SubcategoryId` FOREIGN KEY (`SubcategoryId`) REFERENCES `subcategories` (`Id`);

--
-- Constraints for table `images`
--
ALTER TABLE `images`
  ADD CONSTRAINT `FK_Images_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `maincategories`
--
ALTER TABLE `maincategories`
  ADD CONSTRAINT `FK_MainCategories_MainCategoryImages_ImageId` FOREIGN KEY (`ImageId`) REFERENCES `maincategoryimages` (`Id`);

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `FK_Orders_Addresses_AddressId` FOREIGN KEY (`AddressId`) REFERENCES `addresses` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Orders_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Orders_OrderStatuses_StatusId` FOREIGN KEY (`StatusId`) REFERENCES `orderstatuses` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Orders_PaymentTypes_PaymentTypeId` FOREIGN KEY (`PaymentTypeId`) REFERENCES `paymenttypes` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `pricehistories`
--
ALTER TABLE `pricehistories`
  ADD CONSTRAINT `FK_PriceHistories_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `FK_Products_Brands_BrandId` FOREIGN KEY (`BrandId`) REFERENCES `brands` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Products_Models_ModelId` FOREIGN KEY (`ModelId`) REFERENCES `models` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Products_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`),
  ADD CONSTRAINT `FK_Products_Subcategories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `subcategories` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Products_WishLists_WishListId` FOREIGN KEY (`WishListId`) REFERENCES `wishlists` (`Id`);

--
-- Constraints for table `questionanswers`
--
ALTER TABLE `questionanswers`
  ADD CONSTRAINT `FK_QuestionAnswers_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_QuestionAnswers_Questions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `FK_Questions_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Questions_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `reviewanswers`
--
ALTER TABLE `reviewanswers`
  ADD CONSTRAINT `FK_ReviewAnswers_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_ReviewAnswers_Reviews_ReviewId` FOREIGN KEY (`ReviewId`) REFERENCES `reviews` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `reviews`
--
ALTER TABLE `reviews`
  ADD CONSTRAINT `FK_Reviews_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Reviews_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`);

--
-- Constraints for table `subcategories`
--
ALTER TABLE `subcategories`
  ADD CONSTRAINT `FK_Subcategories_MainCategories_MainCategoryId` FOREIGN KEY (`MainCategoryId`) REFERENCES `maincategories` (`Id`),
  ADD CONSTRAINT `FK_Subcategories_SubcategoryImages_ImageId` FOREIGN KEY (`ImageId`) REFERENCES `subcategoryimages` (`Id`);

--
-- Constraints for table `userlogs`
--
ALTER TABLE `userlogs`
  ADD CONSTRAINT `FK_UserLogs_AspNetUsers_SubjectUserId` FOREIGN KEY (`SubjectUserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UserLogs_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UserLogs_LoggingActions_LoggingActionId` FOREIGN KEY (`LoggingActionId`) REFERENCES `loggingactions` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `wishlists`
--
ALTER TABLE `wishlists`
  ADD CONSTRAINT `FK_WishLists_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

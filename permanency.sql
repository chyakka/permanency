-- --------------------------------------------------------
-- Host:                         209.97.178.118
-- Server version:               5.7.26-0ubuntu0.18.10.1 - (Ubuntu)
-- Server OS:                    Linux
-- HeidiSQL Version:             10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for assignment2
CREATE DATABASE IF NOT EXISTS `assignment2` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `assignment2`;

-- Dumping structure for table assignment2.Users
CREATE TABLE IF NOT EXISTS `Users` (
  `Username` varchar(255) NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Password` longtext,
  `Email` longtext,
  `Moderator` bit(1) NOT NULL DEFAULT b'0',
  `Developer` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Username`),
  UNIQUE KEY `AK_Users_Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

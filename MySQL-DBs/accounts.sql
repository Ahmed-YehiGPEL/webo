/*
Navicat MySQL Data Transfer

Source Server         : CONN
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : accounts

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2014-12-30 12:58:31
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for actkeys
-- ----------------------------
DROP TABLE IF EXISTS `actkeys`;
CREATE TABLE `actkeys` (
  `ID` mediumint(9) NOT NULL,
  `actKey` mediumtext CHARACTER SET ascii NOT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for emailnotifcations
-- ----------------------------
DROP TABLE IF EXISTS `emailnotifcations`;
CREATE TABLE `emailnotifcations` (
  `SeatID` varchar(11) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`SeatID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for forumnumber
-- ----------------------------
DROP TABLE IF EXISTS `forumnumber`;
CREATE TABLE `forumnumber` (
  `ID` mediumint(9) NOT NULL,
  `forumNumber` mediumint(9) NOT NULL,
  PRIMARY KEY (`forumNumber`),
  UNIQUE KEY `forumNumber` (`forumNumber`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for fullname
-- ----------------------------
DROP TABLE IF EXISTS `fullname`;
CREATE TABLE `fullname` (
  `ID` mediumint(8) unsigned NOT NULL,
  `fullName` text NOT NULL,
  UNIQUE KEY `ID` (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for student_activities
-- ----------------------------
DROP TABLE IF EXISTS `student_activities`;
CREATE TABLE `student_activities` (
  `ID` int(11) NOT NULL,
  `acName` varchar(255) NOT NULL,
  `acDescription` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii;

-- ----------------------------
-- Table structure for student_id
-- ----------------------------
DROP TABLE IF EXISTS `student_id`;
CREATE TABLE `student_id` (
  `ID` int(11) NOT NULL,
  `Reserved` int(255) DEFAULT NULL,
  `SeatID` bigint(10) NOT NULL,
  `forumNum` bigint(5) DEFAULT NULL,
  PRIMARY KEY (`ID`,`SeatID`)
) ENGINE=MyISAM DEFAULT CHARSET=ascii;

-- ----------------------------
-- Table structure for uacc
-- ----------------------------
DROP TABLE IF EXISTS `uacc`;
CREATE TABLE `uacc` (
  `ID` bigint(10) unsigned NOT NULL,
  `fullName` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `acPass` text CHARACTER SET latin1 NOT NULL,
  `pNum` int(11) NOT NULL,
  `Email` text CHARACTER SET latin1 NOT NULL,
  `nickName` text CHARACTER SET latin1 NOT NULL,
  `acmLevel` varchar(255) CHARACTER SET latin1 NOT NULL,
  `stActivity` varchar(255) CHARACTER SET latin1 NOT NULL,
  `acKey` varchar(255) NOT NULL,
  `codeforcesHandle` varchar(255) NOT NULL,
  `isActive` text NOT NULL,
  `regDate` text NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `pNum` (`pNum`,`acKey`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Function structure for Empty
-- ----------------------------
DROP FUNCTION IF EXISTS `Empty`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `Empty`(`Field Name` text) RETURNS int(11)
BEGIN
	#Routine body goes here...
	
	RETURN 0;
END
;;
DELIMITER ;

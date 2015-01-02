/*
Navicat MySQL Data Transfer

Source Server         : CONN
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : accounts

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-01-02 23:44:32
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for acc_status
-- ----------------------------
DROP TABLE IF EXISTS `acc_status`;
CREATE TABLE `acc_status` (
  `ID` int(11) NOT NULL,
  `State` varchar(2) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
-- Table structure for secquestions
-- ----------------------------
DROP TABLE IF EXISTS `secquestions`;
CREATE TABLE `secquestions` (
  `Num` mediumint(9) NOT NULL AUTO_INCREMENT,
  `scQuestion` varchar(255) CHARACTER SET ascii NOT NULL,
  PRIMARY KEY (`Num`),
  UNIQUE KEY `ID` (`Num`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

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
  `regDate` timestamp NOT NULL,
  `secQuestion` varchar(255) NOT NULL,
  `secAnswer` varchar(255) NOT NULL,
  `loginState` varchar(2) NOT NULL,
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

-- ----------------------------
-- Event structure for delete_nonActive
-- ----------------------------
DROP EVENT IF EXISTS `delete_nonActive`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` EVENT `delete_nonActive` ON SCHEDULE EVERY 1 MINUTE STARTS '2015-01-01 00:00:00' ON COMPLETION NOT PRESERVE ENABLE DO BEGIN
		DELETE FROM uacc WHERE  uacc.regDate <= TIMESTAMP(DATE_SUB(NOW(), INTERVAL 3 DAY)) && uacc.isActive ='N';
	END
;;
DELIMITER ;

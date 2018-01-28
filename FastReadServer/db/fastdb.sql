/*
SQLyog 企业版 - MySQL GUI v8.14 
MySQL - 5.1.62-community : Database - fastdb
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`fastdb` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `fastdb`;

/*Table structure for table `td_anima` */

DROP TABLE IF EXISTS `td_anima`;

CREATE TABLE `td_anima` (
  `img_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `file_name` varchar(100) DEFAULT NULL,
  `create_time` datetime DEFAULT NULL,
  PRIMARY KEY (`img_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_anima` */

/*Table structure for table `td_code` */

DROP TABLE IF EXISTS `td_code`;

CREATE TABLE `td_code` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `category` varchar(50) DEFAULT NULL,
  `categoryname` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

/*Data for the table `td_code` */

insert  into `td_code`(`id`,`category`,`categoryname`) values (1,'01','1000字以下每分钟'),(2,'02','1000-2000字每分钟'),(3,'03','2000-3000字每分钟'),(14,'04','3000-4000字每分钟'),(15,'05','4000-5000字每分钟'),(16,'06','5000-6500字每分钟'),(17,'07','6500-8000字每分钟'),(18,'08','8000-9500字每分钟'),(19,'09','9500-11000字每分钟'),(20,'10','11000-12500字每分钟'),(21,'11','12500-14000字每分钟'),(22,'12','14000-15000字每分钟'),(23,'13','15000-16000字每分钟'),(24,'14','16000-17000字每分钟');

/*Table structure for table `td_license` */

DROP TABLE IF EXISTS `td_license`;

CREATE TABLE `td_license` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(32) DEFAULT NULL,
  `mac` varchar(30) DEFAULT NULL,
  `use_time` datetime DEFAULT NULL,
  `state` varchar(1) DEFAULT NULL COMMENT '1:正常 2:停用',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_license` */

/*Table structure for table `td_log` */

DROP TABLE IF EXISTS `td_log`;

CREATE TABLE `td_log` (
  `log_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(32) DEFAULT NULL,
  `login_time` datetime DEFAULT NULL,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_log` */

/*Table structure for table `td_question` */

DROP TABLE IF EXISTS `td_question`;

CREATE TABLE `td_question` (
  `q_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `q_type` varchar(1) DEFAULT NULL,
  `train_id` bigint(20) DEFAULT NULL,
  `op1` varchar(100) DEFAULT NULL,
  `op2` varchar(100) DEFAULT NULL,
  `op3` varchar(100) DEFAULT NULL,
  `op4` varchar(100) DEFAULT NULL,
  `answer` varchar(1) DEFAULT NULL,
  `create_time` datetime DEFAULT NULL,
  PRIMARY KEY (`q_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_question` */

/*Table structure for table `td_training` */

DROP TABLE IF EXISTS `td_training`;

CREATE TABLE `td_training` (
  `train_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `train_type` varchar(1) DEFAULT NULL COMMENT '1:课堂训练 2:阅读测评',
  `title` varchar(100) DEFAULT NULL,
  `photo` varchar(100) DEFAULT NULL,
  `content` text,
  `words` int(8) DEFAULT NULL,
  `speed` varchar(2) DEFAULT NULL,
  `create_time` datetime DEFAULT NULL,
  PRIMARY KEY (`train_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_training` */

/*Table structure for table `td_update_log` */

DROP TABLE IF EXISTS `td_update_log`;

CREATE TABLE `td_update_log` (
  `upd_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `code` varchar(32) DEFAULT NULL,
  `last_time` datetime DEFAULT NULL,
  PRIMARY KEY (`upd_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_update_log` */

/*Table structure for table `td_user` */

DROP TABLE IF EXISTS `td_user`;

CREATE TABLE `td_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `login_name` varchar(20) DEFAULT NULL,
  `pwd` varchar(40) DEFAULT NULL,
  `state` varchar(1) DEFAULT NULL,
  `login_time` datetime DEFAULT NULL,
  `nick_name` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `td_user` */

insert  into `td_user`(`id`,`login_name`,`pwd`,`state`,`login_time`,`nick_name`) values (1,'admin','21232F297A57A5A743894A0E4A801FC3','1',NULL,'管理员');

/*Table structure for table `td_view_training` */

DROP TABLE IF EXISTS `td_view_training`;

CREATE TABLE `td_view_training` (
  `viewtrain_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `vt_type` varchar(1) DEFAULT NULL,
  `title` varchar(100) DEFAULT NULL,
  `route` text,
  `desc` text,
  `content` text,
  `create_time` datetime DEFAULT NULL,
  PRIMARY KEY (`viewtrain_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `td_view_training` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

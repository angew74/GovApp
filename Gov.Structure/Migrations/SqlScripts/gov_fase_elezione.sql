-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: gov
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `fase_elezione`
--

LOCK TABLES `fase_elezione` WRITE;
/*!40000 ALTER TABLE `fase_elezione` DISABLE KEYS */;
INSERT INTO `fase_elezione` VALUES (27,'CO','Costituzione',1,1,'AFF'),(30,'1A','1 Affluenza',1,1,'AFF'),(31,'2A','2 Affluenza',1,1,'AFF'),(32,'3C','Chiusura',1,1,'AFF'),(33,'3A','3 Affluenza',0,1,'AFF'),(34,'4C','Chiusura 4 Affluenza',0,1,'AFF'),(35,'VL','Voti Lista',1,1,'LIS'),(36,'VP','Voti Presidente',0,3,'PRE'),(37,'VS','Voti Sindaco',1,4,'SIN'),(38,'VU','Voti Uninominalie',0,2,'LIS'),(39,'PC','Preferenze Comunali',0,4,'PRF'),(40,'PR','Preferenze Regionali',0,3,'PRF'),(41,'AP','Apertura',1,1,'RAF'),(42,'RCO','Rettifica Costituzione',1,1,'RAF'),(43,'RAP','Rettiifica Apertura',1,1,'RAF'),(44,'R1A','Rettifica 1 Affluenza',1,1,'RAF'),(45,'R2A','Rettifica 2 Affluenza',0,1,'RAF'),(46,'R3C','Rettifica Chiusura',0,1,'RAF'),(47,'RVL','Rettifica Voti Lista',0,1,'LIS'),(48,'RPE','Rettifica Preferenze Europee',0,1,'PRF'),(49,'PE','Preferenze Europee',1,1,'PRF'),(50,'RIC','Ricalcoli Affluenze',1,1,'RIC'),(51,'RIL','Ricalcoli Voti Lista',1,1,'RIC'),(52,'RIP','Ricalcoli Preferenze',1,1,'RIC'),(53,'RIA','Ricalcoli Costituzione Apertura',1,1,'RIC'),(54,'RIS','Ricalcolo Sindaco',0,4,'RIC'),(56,'RPI','Ricalcolo Presidente',0,3,'RIC'),(57,'RCS','Ricalcolo Coalizioni Sindaco',0,4,'RIC'),(58,'RCP','Ricalcolo Coalizioni Presidente',0,3,'RIC'),(72,'CO','Costituzione Seggio',1,4,'AFF'),(73,'1A','1 Affluenza',1,4,'AFF'),(74,'2A','2 Affluenza',1,4,'AFF'),(75,'3C','Chiusura Affluenza',1,4,'AFF'),(76,'AP','Apertura Seggio',1,4,'AFF'),(77,'RAP','Rettifica Apertura',1,4,'RAF'),(78,'R1A','Rettifica 1 Affluenza',1,4,'RAF'),(79,'R2A','Rettifica 2 Affluenza',1,4,'RAF'),(80,'R3C','Rettifica Chiusura Affluenza',1,4,'RAF'),(81,'VL','Voti Lista',0,4,'LIS'),(82,'RVL','Rettifica Voti Lista',0,4,'LIS'),(83,'RIL','Ricalcolo Voti Lista',0,4,'RIC'),(84,'RIP','Ricalcolo Preferenze',0,4,'RIC'),(85,'RVS','Rettifica Voti Sindaco',1,4,'SIN');
/*!40000 ALTER TABLE `fase_elezione` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-08-12  9:52:47

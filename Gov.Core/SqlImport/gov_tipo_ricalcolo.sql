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
-- Dumping data for table `tipo_ricalcolo`
--

LOCK TABLES `tipo_ricalcolo` WRITE;
/*!40000 ALTER TABLE `tipo_ricalcolo` DISABLE KEYS */;
INSERT INTO `tipo_ricalcolo` VALUES (1,'Ricalcolo 1 Affluenza','RIC','AF1',1),(2,'Ricalcolo 2 Affluenza','RIC','AF2',1),(3,'Ricalcolo Chiusura','RIC','CHI',1),(4,'Ricalcolo 1 Apertura ','RIA','AP1',1),(5,'Ricalcolo 2 Apertura','RIA','AP2',3),(6,'Ricalcolo 1 Costituzione','RIA','CO1',1),(7,'Ricalcolo 2 Costituzione','RIA','CO2',3),(11,'Ricalcolo Voti Lista','RIL','VL',1),(12,'Ricalcolo Preferenze','RIP','VP',1),(13,'Ricalcolo Coalizioni','RIO','VC',3),(14,'Ricalcolo Preferenze','RIP','VP',3),(15,'Ricalcolo Voti Lista','RIL','VL',3),(16,'Ricalcolo Voti Lista','RIL','VL',4),(17,'Ricalcolo Sindaco','RIS','VS',3),(18,'Ricalcolo 1 Affluenza','RIC','AF1',3),(19,'Ricalcolo 2 Affluenza','RIC','AF2',3),(20,'Ricalcolo Chiusura','RIC','CHI',3),(21,'Ricalcolo 1 Costituzione','RIA','CO1',3),(22,'Ricalcolo 1 Apertura','RIA','AP1',3);
/*!40000 ALTER TABLE `tipo_ricalcolo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-08-12  9:52:54

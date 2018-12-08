-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Dec 08, 2018 at 12:50 AM
-- Server version: 5.6.34-log
-- PHP Version: 7.2.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `kenny_wolfenberger`
--
CREATE DATABASE IF NOT EXISTS `kenny_wolfenberger` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `kenny_wolfenberger`;

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `stylist_id` int(11) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `notes` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `clients`
--

INSERT INTO `clients` (`id`, `name`, `stylist_id`, `phone`, `notes`) VALUES
(1, 'Jane Doe', 1, '555-555-5555', 'Prefers Aveda hair products'),
(2, 'Jim Doe', 1, '555-555-5555', 'This client prefers... \r\nTrimmers level 2 on sides, scissors cut on top. \r\n    '),
(3, 'Jon', 5, '555-555-5555', 'This client prefers... super short hair. The shorter the better!\r\n    '),
(4, 'Kim Kardashian', 6, '444-456-4565', 'This client prefers... privacy! Please do not let in the paparazzi. '),
(6, 'Willy Wonka', 3, '234-444-4444', 'This client prefers... a crazy haircut and free candies. \r\n    '),
(7, 'Jill Jones', 5, '444-555-3456', 'This client prefers... long walks on the beach.\r\n    '),
(9, 'Emily Dickinson', 4, '333-333-3333', 'This client prefers... old fashioned hairstyles  '),
(10, 'Paul Bunyan', 2, '234-234-2344', 'This client prefers... big scissors Jim\r\n    '),
(12, 'Jen', 6, '234-4444', 'This client prefers... long hair styles and lots of colors\r\n    '),
(13, 'Frank ', 1, '321-4444', 'This client prefers... no preferences\r\n    '),
(14, 'Fanny May', 2, '444-2342', 'This client prefers... no preferences at this time'),
(15, 'Anna', 3, '666-4565', 'This client prefers... no preferences at this time\r\n    '),
(16, 'Annabell', 4, '420-4747', 'This client prefers... no preferences at this time\r\n    '),
(17, 'Donald', 5, '777-7777', 'None at this time\r\n    '),
(18, 'Meek', 6, '474-3333', 'This client prefers... for nothing to change\r\n    ');

-- --------------------------------------------------------

--
-- Table structure for table `stylists`
--

CREATE TABLE `stylists` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `picture` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stylists`
--

INSERT INTO `stylists` (`id`, `name`, `phone`, `picture`) VALUES
(1, 'Tammy Scissorhands', '555-555-5555', 'sample_picture'),
(2, '\"Big Scissors\" Jim', '555-555-12345', 'jj'),
(3, 'Alfred the Butler', '555-555-5555', 'alfred'),
(4, 'Cathy Hairmaster', '555-555-1234', 'cathy'),
(5, 'Brian Buzzcut', '1-800-555-1111', 'brian'),
(6, 'Fran McTangles', '555-555-8787', 'fran');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `stylists`
--
ALTER TABLE `stylists`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clients`
--
ALTER TABLE `clients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT for table `stylists`
--
ALTER TABLE `stylists`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

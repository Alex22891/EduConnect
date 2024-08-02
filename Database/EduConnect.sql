-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Май 28 2024 г., 11:04
-- Версия сервера: 8.0.30
-- Версия PHP: 8.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `EduConnect`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Coaches`
--

CREATE TABLE `Coaches` (
  `id` int NOT NULL,
  `FullName` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Дамп данных таблицы `Coaches`
--

INSERT INTO `Coaches` (`id`, `FullName`) VALUES
(1, 'Жемалутдинова И.Б.'),
(2, 'Локтеева А.В.');

-- --------------------------------------------------------

--
-- Структура таблицы `Competitions`
--

CREATE TABLE `Competitions` (
  `id` int NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `sport_type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `event_date` datetime DEFAULT NULL,
  `participants_count` int DEFAULT NULL,
  `results` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `Year` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Дамп данных таблицы `Competitions`
--

INSERT INTO `Competitions` (`id`, `name`, `sport_type`, `event_date`, `participants_count`, `results`, `Year`) VALUES
(4, 'пер-во Забайкальского края', 'дзюдо', '2011-11-11 00:00:00', 24, 'иванов -1 место, 3.\r\n', 2011);

-- --------------------------------------------------------

--
-- Структура таблицы `Events`
--

CREATE TABLE `Events` (
  `Id` int NOT NULL,
  `Title` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Level` varchar(100) DEFAULT NULL,
  `Date` date DEFAULT NULL,
  `Amount` varchar(100) DEFAULT NULL,
  `Result` varchar(100) DEFAULT NULL,
  `Year` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Структура таблицы `Groups`
--

CREATE TABLE `Groups` (
  `Id` int NOT NULL,
  `Title` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Дамп данных таблицы `Groups`
--

INSERT INTO `Groups` (`Id`, `Title`) VALUES
(1, 'СОГ-1'),
(2, 'СОГ-2'),
(3, 'СОГ-3'),
(4, 'СОГ-4'),
(5, 'СОГ-5'),
(6, 'СОГ-6'),
(7, 'ГНП-1'),
(8, 'ГНП-2'),
(9, 'ГНП-3'),
(10, 'ГНП-4'),
(11, 'ГНП-5'),
(12, 'ГНП-6'),
(13, 'ОТГ-1'),
(14, 'ОТГ-2'),
(15, 'ОТГ-3'),
(16, 'ОТГ-4'),
(17, 'ОТГ-5'),
(18, 'ОТГ-6'),
(19, 'ГСС-1'),
(20, 'ГСС-2'),
(21, 'ГСС-3'),
(22, 'ГСС-4'),
(23, 'ГСС-5'),
(24, 'ГСС-6');

-- --------------------------------------------------------

--
-- Структура таблицы `History`
--

CREATE TABLE `History` (
  `ID` int NOT NULL,
  `FullName` varchar(225) DEFAULT NULL,
  `Rank` varchar(225) DEFAULT NULL,
  `Competitions` varchar(225) DEFAULT NULL,
  `Norms` varchar(225) DEFAULT NULL,
  `Year` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Структура таблицы `Norms`
--

CREATE TABLE `Norms` (
  `id` int NOT NULL,
  `FullName` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SportName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `YearComplete` int DEFAULT NULL,
  `TrainerName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `NormsAtTheBeginning` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `NormsAtTheEndOf` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SurrenderRate` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Comment` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Sports`
--

CREATE TABLE `Sports` (
  `id` int NOT NULL,
  `SportName` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Дамп данных таблицы `Sports`
--

INSERT INTO `Sports` (`id`, `SportName`) VALUES
(1, 'Дзюдо'),
(2, 'Баскетбол'),
(3, 'Вольная борьба'),
(4, 'Чир спорт'),
(5, 'Ушу'),
(6, 'Бадминтон');

-- --------------------------------------------------------

--
-- Структура таблицы `Students`
--

CREATE TABLE `Students` (
  `id` int NOT NULL,
  `Surname` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Name` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Patronymic` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BirthDate` date NOT NULL,
  `School` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Class` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sport` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TrainersName` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EnrollmentGroup` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Rank` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OrderNumber` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `DateOfEnrollment` date DEFAULT NULL,
  `OrderEnrollment` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OrderDismissal` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PaymentType` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ORP_or_SP` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ParentsFullName` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PhoneNumber` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Address` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ParentsWorkPlace` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ParentsPosition` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `BirthCertificate` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `DateOfIssue` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `IssuedBy` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `SNILS` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `INN` varchar(225) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Дамп данных таблицы `Students`
--

INSERT INTO `Students` (`id`, `Surname`, `Name`, `Patronymic`, `BirthDate`, `School`, `Class`, `Sport`, `TrainersName`, `EnrollmentGroup`, `Rank`, `OrderNumber`, `DateOfEnrollment`, `OrderEnrollment`, `OrderDismissal`, `PaymentType`, `ORP_or_SP`, `ParentsFullName`, `PhoneNumber`, `Address`, `ParentsWorkPlace`, `ParentsPosition`, `BirthCertificate`, `DateOfIssue`, `IssuedBy`, `SNILS`, `INN`) VALUES
(1, 'Aбидуева', 'Юлия', 'Андреевна', '2012-03-07', '52', '5', 'Чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.05.2023', '2020-09-01', '51', '', 'Бюджет', 'СП', 'Абидуева Анастасия Алексеевна', '89243777091', 'г. Чита,  ул. Космонавтов д.9 кв.24', 'КГАУ МФЦ', 'администратор', 'I-СП № 831471', '2012-04-13 00:00:00', 'отдел ЗАГС Читинског района Департамента ЗАГС Забайкальского края', '173-182-114 48', '7596789792000211, 21.05.2012'),
(2, 'Гильмутдинова ', 'Александра', 'Семеновна', '2013-12-10', '52', '4', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.05.2023', '2021-09-01', '33', '', 'бюджет', 'спортивная подготовка', 'Гильмутдинова Вера Валерьевна', '89141438784', 'г. Чита,  3 мкр., д.10 кв.15', 'д/х', '', 'I-СП № 656459', '2019-11-18 00:00:00', 'отдел ЗАГС Борзинского района Департамента ЗАГС Забайкальского края', '179-742-241 11', '7587689789000060. 25.05.2014'),
(3, 'Гильмутдинова', 'Екатерина', 'Семеновна', '2012-06-24', '52', '4', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№146 07.11.2023', '2021-09-01', '33', '', 'бюджет', 'спортивная подготовка', 'Гильмутдинова Вера Валерьевна', '89141438784', 'г. Чита,  3мкр.. д.10 кв.15', 'д/х', '', 'I-СП № 656458', '2019-11-18 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-646-864 98', '7593789775000041,05.09.2012'),
(4, 'Гроховская', 'Виктория', 'Романовна', '2012-06-28', '8', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Гроховская Валерия Александровна', '89145177337', 'г. Чита, 4 мкр д.33 кв. 67', 'в/ч 63559', 'механик', 'I-СП № 838733', '2012-07-04 00:00:00', 'отделЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-832-051 60', '7593789775000041, 05.09.2012'),
(5, 'Евстафьева', 'Мирослава', 'Алексеевна', '2012-12-23', '26', '4', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Евстафьева Евгения Викторовна', '89248146281', 'г. Чита,  ул. Текстильщиков д.3 кв.56', 'МБДУ ОЧ №34', 'учитель-логопед', 'I-СП № 865856', '2013-01-09 00:00:00', 'отдел ЗАГС Центрального района г. Читы Департамента ЗАГС Забайкальского края', '176-125-139 60 ', '7587789776000097, 23.12 2012'),
(6, 'Коноваленко', 'Мария', 'Сергеевна', '2012-08-22', '30', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2021-09-01', '33', '', 'бюджет', 'спортивная подготовка', 'Коноваленко Елена Олеговна', '89144358303', 'г. Чита, ул. Автозаводская. д.5 кв.23', 'н/р', '', 'I-СП № 851569', '2012-08-28 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-167-903 72', '7594789770000052, 01.11.2012'),
(7, 'Кузьмина', 'Софья', 'Валерьевна', '2012-01-26', '8', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Кузьмина Луиза Геннадьевна', '89245030546', 'г. Чита, пр-т Фадеева Д.10 кв.71', '321ВКГ Минобороны России', 'медицинская сестра', 'I-СП № 838160', '2012-02-06 00:00:00', 'отдел ЗАГС Черновского района г.Читы Департамента ЗАГС Забайкальского края', '170-411-387 29', '7598789773000055, 02.04.2012'),
(8, 'Макарова', 'Ксения', 'Александровна', '2012-05-19', '52', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Макаров Александр Вячеславович', '89963127259', 'г. Чита, 5мкр. д.35 кв.113', 'самозанятый', '', 'I-СП № 838590', '2012-05-31 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-107-930 45', '7594789780000092, 03.09.2012'),
(9, 'Номаконова', 'Мария', 'Сергеевна', '2012-05-29', '52', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Намаконова Елена Витальевна', '89243746933', 'г. Чита, ул. Космонавтов д.5 кв. 8', 'ЧУЗ КБ РЖД-медицина', 'фельдшер-лаборант', 'I-СП № 849930', '2012-06-04 00:00:00', 'отделЗАГС Центрального района г.Читы Департамента ЗАГС Забайкальского края', '174-101-595 40', '7591789777000025, 26.06.2012'),
(10, 'Потемкина', 'Мария', 'Дмитриевна', '2012-01-03', '52', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Потемкина Ольга Алексеевна', '89144868285', 'г. Чита, 3 мкр. д.11 кв. 79', 'ООО\"Локотех-сервис\"', 'инженер по подготовке кадров', 'I-СП № 838086', '2012-01-19 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '170-337-703 49', '7598789796000132, 16.02.2012'),
(11, 'Размахнина ', 'Екатерина', 'Александровна', '2012-10-30', '30', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Размахнина Анна Федоровна', '89996853101', 'г. Чита, пр-т Фадеева д.10 кв.172', 'МБОУ СОШ №30', 'учитель', 'I-CП № 505532', '2014-03-05 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '174-473-528 90', '7589789769000144, 01.12.2012'),
(12, 'Рычкова', 'Арина', 'Витальевна', '2012-11-19', '8', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2023-09-01', '', '', 'бюджет', 'спортивная подготовка', 'Рычкова Лилия Рифовна', '89145251461', 'г. Чита, пр-т Фадеева д.16 кв.125', 'ООО \"Фарммедтехника\"', 'продавец', 'I-СП № 867029', '2012-11-28 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '174-546-952 04', '7588789780000058, 18.12.2012'),
(13, 'Сапожникова', 'Валерия ', 'Денисовна', '2012-05-22', '62', '6', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Сапожникова Анастасия Валерьевна', '89644715582', 'г. Чита, ул. Н-Широких д.5 кв.48', 'ГУЗ ДЛМЦ', 'фельдшер', 'I-СП № 838595', '2012-05-30 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-317-069 50', '7594789777000063, 23.07.2012'),
(14, 'Сомова', 'Алина', 'Николаевна', '2012-04-06', '52', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Сомов Николай Алескандрович', '89963110388', 'г. Чита, 3 мкр. д.8 кв.20', 'ОАО РЖД ', 'машинист', 'I -СП № 838398', '2012-04-12 00:00:00', 'отдел ЗАГС Чесновского района г. Читы Департамента ЗАГС Забайкальского края', '171-405-073 32', '7595789793000138, 04.06.2012'),
(15, 'Федорова ', 'Елена', 'Сергеевна', '2012-01-19', '8', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Федоров Сергей Константинович', '89145148130', 'г. Чита, 6 мкр. д.1 кв.41', 'ОАО \"Эльга Транс\"', '', 'I-CП № 823599', '2012-01-27 00:00:00', 'отдел ЗАГС Могочинского района Департамента ЗАГС Забайкальского края ', '170-084-848 60', '7598789780000189, 23.03.2012'),
(16, 'Фомина', 'Полина', 'Евгеньевна', '2012-05-25', '8', '5', 'чир спорт', 'Жемалутдинова И.Б,', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Фомин Евгений  Александрович', '89144994050', 'г. Чита, пр-т Фадеева д.10 кв.157', 'в/ч 38151', 'командир взвода', 'I-СП № 838594', '2012-06-04 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-245-078 52', '7594789774000082, 14.06.2012'),
(17, 'Шишмарева', 'Анастасия', 'Андреевна', '2012-01-14', '30', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Шишмарева Анна Викторовна', '89141448464', 'г. Чита, 5мкр. д.33 кв.126', 'ГУСОЧКЦСОН \"Берегиня\"', 'специалист по соц. работе', 'I-СП № 838544', '2012-05-17 00:00:00', 'отдел ЗАГС Черновского района г.Читы Департамента ЗАГС Забайкальского края', '170-060-941 30', '7598789785000077, 16.02.2012'),
(18, 'Янькова', 'Арина', 'Вячеславовна', '2012-05-08', '8', '5', 'чир спорт', 'Жемалутдинова И.Б.', 'ГНП-3', '3', '№95 26.03.2023', '2020-09-01', '51', '', 'бюджет', 'спортивная подготовка', 'Янькова Евгения Галимьяновна', '89145020343', 'Г. Чита, 5 мкр. д.32а кв.29', 'ГБСУ СО СРЦ \"Надежда\" ', 'бухгалтер', 'I-СП № 838544', '2012-05-17 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '172-245-316 47', '7594789791000040, 30.05.2012'),
(19, 'Ахрименко', 'Антон', 'Сергеевич', '2009-07-21', '26', '8', 'баскетбол', 'Локтеева А.В.', 'УТГ-2', '3', '№150-П 05.12.2021', '2020-09-01', '26', '', 'бюджет', 'спортивная подготовка', 'Шемелина Татьяна Анатольевна', '89145038020', 'г. Чита, ул. Текстильшиков д.36 кв.62', 'УПРФ г.Читы', '', '7623 292350', '2023-09-26 00:00:00', 'УМВД России по Забайкальскому краю', '168-130-267 58', '7592099728000242, 12.12.2012'),
(20, 'Базаров', 'Егор', 'Андреевич', '2009-04-01', '30', '8', 'баскетбол', 'Локтеева А.В.', 'УТГ-2', '2', '№144-П 07.11.2023', '2018-09-01', '36', '', 'бюджет', 'спортивная подготовка', 'Базарова Аюна Дугаржаповна', '89148045627', 'г. Чита, ул. Весенняя д.6 кв.24', 'ИП \"Базарова М.Д.\"', 'директор', '7623 275651', '2023-04-12 00:00:00', 'УМВД России по Забайкальскому краю', '168-130-518 58', '7595099748000211, 22.02.2013'),
(21, 'Бакшеев', 'Савелий', 'Андреевич', '2010-09-18', '8', '7', 'баскетбол', 'Локтеева А.В.', 'УТГ-2', 'б/р', '', '2024-04-27', '', '', 'бюджет', 'спортивная подготовка', 'Бакшеева Ольга Николаевна', '', '', '', '', 'II-СП  №747663', '2023-10-06 00:00:00', 'отдел ЗАГС Нерченского района Департамента ЗАГС Забайкальского края', '166-327-743 86', '7590989731000092, 14.05.2014'),
(22, 'Зайков', 'Михаил', 'Александрович', '2010-07-07', '30', '7', 'баскетбол', 'Локтеева А.В.', 'УТГ-2', '2', '№144-П 07.11.2023', '2018-09-01', '36', '', 'бюджет', 'спортивная подготовка', 'Зайкова Анастасия Юрьевна', '89243729993', 'г. Чита, 4 мкр. д.27 кв. 53', 'филиал ФГБУ \"Россельхозцентр\"', 'начальник отдела', 'I-СП №783460', '2010-07-15 00:00:00', 'отдел ЗАГС Черновского района г. Читы Департамента ЗАГС Забайкальского края', '168-090-530 78', '7592989742000212, 23.12.2013'),
(23, 'Ионинский', 'Данил', 'Юрьевич', '2009-01-30', '8', '8', 'баскетбол', 'Локтеева А.В.', 'УТГ-2', '2', '№144-П 07.11.2023', '2018-09-01', '36', '', 'бюджет', 'спортивная подготовка', 'Ионинская Ульяна Сергеевна', '89144761152', 'г. Чита, пр-т Фадеева д. 20 кв.12', 'ПАО сбербанк', 'клиеннтский менеджер', '7623 262604', '2023-02-10 00:00:00', 'УМВД России по Забайкальскому краю', '167-854-847 32', '7590099745000102, 25.02.2013');

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `UserID` int NOT NULL,
  `Username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Role` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Дамп данных таблицы `Users`
--

INSERT INTO `Users` (`UserID`, `Username`, `Password`, `Role`) VALUES
(1, 'Admin', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Администратор'),
(2, 'test1', '1b4f0e9851971998e732078544c96b36c3d01cedf7caa332359d6f1d83567014', 'Тренер-преподаватель'),
(3, 'test2', '60303ae22b998861bce3b28f33eec1be758a213c86c93c076dbe9f558c11c752', 'Заместитель директора по УВР'),
(4, 'test3', 'fd61a03af4f77d870fc21e05e7e80678095c92d808cfb3b5c279ee04c74aca13', 'Заместитель директора по СМР'),
(5, 'test4', 'a4e624d686e03ed2767c0abd85c14426b0b1157d2ce81d27bb4fe4f6f01d688a', 'Методист');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Coaches`
--
ALTER TABLE `Coaches`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Competitions`
--
ALTER TABLE `Competitions`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Events`
--
ALTER TABLE `Events`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `Groups`
--
ALTER TABLE `Groups`
  ADD PRIMARY KEY (`Id`);

--
-- Индексы таблицы `History`
--
ALTER TABLE `History`
  ADD PRIMARY KEY (`ID`);

--
-- Индексы таблицы `Norms`
--
ALTER TABLE `Norms`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Sports`
--
ALTER TABLE `Sports`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Students`
--
ALTER TABLE `Students`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`UserID`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Coaches`
--
ALTER TABLE `Coaches`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT для таблицы `Competitions`
--
ALTER TABLE `Competitions`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `Events`
--
ALTER TABLE `Events`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `Groups`
--
ALTER TABLE `Groups`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT для таблицы `History`
--
ALTER TABLE `History`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT для таблицы `Norms`
--
ALTER TABLE `Norms`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT для таблицы `Sports`
--
ALTER TABLE `Sports`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT для таблицы `Students`
--
ALTER TABLE `Students`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `UserID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

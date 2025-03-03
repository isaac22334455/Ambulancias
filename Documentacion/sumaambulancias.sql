-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 03-03-2025 a las 22:03:34
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.1.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sumaambulancias`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cattipoclientes`
--

CREATE TABLE `cattipoclientes` (
  `IdTipoCliente` smallint(6) NOT NULL,
  `vcTipoCliente` varchar(70) NOT NULL,
  `Dt_FechaRegistro` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `cattipoclientes`
--

INSERT INTO `cattipoclientes` (`IdTipoCliente`, `vcTipoCliente`, `Dt_FechaRegistro`) VALUES
(2, 'Pediatrico', '2024-11-03'),
(6, 'Adulto', '2024-11-09'),
(7, 'Adulto mayor', '2024-11-09');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `Id_Cliente` int(11) NOT NULL,
  `Nombre_Cliente` varchar(50) NOT NULL,
  `AppMat_Cliente` varchar(20) NOT NULL,
  `AppPat_Cliente` varchar(20) NOT NULL,
  `Tel_Cliente` varchar(12) NOT NULL,
  `Correo_Cliente` varchar(50) NOT NULL,
  `IdTipoMembrecia` int(11) NOT NULL,
  `Fecha_Inicio` date DEFAULT NULL,
  `Fecha_Fin` date DEFAULT NULL,
  `dt_FechaRegistro` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`Id_Cliente`, `Nombre_Cliente`, `AppMat_Cliente`, `AppPat_Cliente`, `Tel_Cliente`, `Correo_Cliente`, `IdTipoMembrecia`, `Fecha_Inicio`, `Fecha_Fin`, `dt_FechaRegistro`) VALUES
(13, 'Panfilo', 'Escamilla', 'Lopez', '98999999', 'ssss', 9, '2024-11-12', '2024-12-12', '2024-11-10'),
(14, 'Jose Maria', 'Gomez', 'Perez', '8129372687', 'Jose23@gmail.com.mx', 10, '2024-11-12', '2025-11-12', '2024-11-12');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empleados`
--

CREATE TABLE `empleados` (
  `Id_Empleado` int(11) NOT NULL COMMENT 'id del empleado',
  `AppPat_Empleado` varchar(20) NOT NULL COMMENT 'appellido paterno',
  `AppMat_Empleado` varchar(20) NOT NULL COMMENT 'apellido materno',
  `Nombre_Empleado` varchar(40) NOT NULL COMMENT 'Nombres ',
  `Tel_Empleado` varchar(12) NOT NULL COMMENT 'numero de telefono',
  `correo_Empleado` varchar(100) NOT NULL COMMENT 'correo',
  `CostHor_Empleado` decimal(10,2) NOT NULL COMMENT 'costo de hora',
  `dt_FechaRegistro` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `empleados`
--

INSERT INTO `empleados` (`Id_Empleado`, `AppPat_Empleado`, `AppMat_Empleado`, `Nombre_Empleado`, `Tel_Empleado`, `correo_Empleado`, `CostHor_Empleado`, `dt_FechaRegistro`) VALUES
(14, 'Hernandez', 'Gonzalez', 'Erick', '8129376789', 'ErickHernandez55@gmail.com', '120.00', '2024-11-12');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `entradasmembrecias`
--

CREATE TABLE `entradasmembrecias` (
  `Id_Entrada` int(11) NOT NULL,
  `MCantidad` decimal(18,2) NOT NULL,
  `Fecha_Inicio` date NOT NULL DEFAULT current_timestamp(),
  `dt_FechaRegistro` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `entradasmembrecias`
--

INSERT INTO `entradasmembrecias` (`Id_Entrada`, `MCantidad`, `Fecha_Inicio`, `dt_FechaRegistro`) VALUES
(12, '70000.00', '2024-11-10', '2024-11-10'),
(15, '30000.00', '2024-11-12', '2024-11-12');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `horaspagadas`
--

CREATE TABLE `horaspagadas` (
  `Id_HoraPagada` int(11) NOT NULL,
  `Id_Empleado` int(11) NOT NULL,
  `PagoTotal` decimal(10,0) NOT NULL,
  `iHorasPagadas` int(11) NOT NULL,
  `Feha_Registro` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `horaspagadas`
--

INSERT INTO `horaspagadas` (`Id_HoraPagada`, `Id_Empleado`, `PagoTotal`, `iHorasPagadas`, `Feha_Registro`) VALUES
(8, 8, '2160', 18, '2024-11-02'),
(9, 8, '600', 5, '2024-11-02'),
(10, 8, '2520', 21, '2024-11-02'),
(14, 12, '1407', 14, '2024-11-02'),
(15, 8, '2160', 18, '2024-11-03'),
(16, 12, '2111', 21, '2024-11-09'),
(17, 12, '1910', 19, '2024-11-09'),
(18, 14, '1008', 9, '2024-11-12'),
(19, 14, '2040', 17, '2024-11-12'),
(21, 14, '2160', 18, '2024-11-15');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `horastrabajadas`
--

CREATE TABLE `horastrabajadas` (
  `Id_Horas` int(11) NOT NULL,
  `Id_Empleado` int(11) NOT NULL,
  `iHorasTrabajadas` tinyint(4) NOT NULL,
  `dt_Fecha` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `horastrabajadas`
--

INSERT INTO `horastrabajadas` (`Id_Horas`, `Id_Empleado`, `iHorasTrabajadas`, `dt_Fecha`) VALUES
(37, 12, 9, '2024-11-09');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `membrecias`
--

CREATE TABLE `membrecias` (
  `Id_Membrecia` int(11) NOT NULL,
  `IFrecMen_Membrecia` smallint(6) NOT NULL,
  `Id_TipoCliente` int(11) NOT NULL,
  `Dt_FechaRegistro` date NOT NULL DEFAULT current_timestamp(),
  `MCantidadCobrar` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `membrecias`
--

INSERT INTO `membrecias` (`Id_Membrecia`, `IFrecMen_Membrecia`, `Id_TipoCliente`, `Dt_FechaRegistro`, `MCantidadCobrar`) VALUES
(8, 3, 6, '2024-11-10', '70000.00'),
(9, 1, 6, '2024-11-10', '1500.00'),
(10, 12, 6, '2024-11-10', '30000.00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `Id_Rol` smallint(6) NOT NULL COMMENT 'id del rol',
  `Nombre_Rol` varchar(50) NOT NULL COMMENT 'nombre del rol',
  `Llave_Rol` varchar(50) NOT NULL COMMENT 'LLave del rol'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `salidas`
--

CREATE TABLE `salidas` (
  `Id_Salida` int(11) NOT NULL,
  `VcMotivoSalida` varchar(100) NOT NULL,
  `MmontoSalida` decimal(18,2) NOT NULL,
  `dt_FechaRegistro` date NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `salidas`
--

INSERT INTO `salidas` (`Id_Salida`, `VcMotivoSalida`, `MmontoSalida`, `dt_FechaRegistro`) VALUES
(1, 'Pago de honorarios', '960.00', '2024-10-31'),
(2, 'Pago de honorarios', '1080.00', '2024-11-02'),
(3, 'Pago de honorarios', '1080.00', '2024-11-02'),
(4, 'Pago de honorarios', '960.00', '2024-11-02'),
(5, 'Pago de honorarios', '2160.00', '2024-11-02'),
(6, 'Pago de honorarios', '1080.00', '2024-11-02'),
(7, 'Pago de honorarios', '3000.00', '2024-11-02'),
(8, 'Pago de honorarios', '2160.00', '2024-11-02'),
(9, 'Pago de honorarios', '600.00', '2024-11-02'),
(10, 'Pago de honorarios', '2520.00', '2024-11-02'),
(11, 'Pago de honorarios', '1080.00', '2024-11-02'),
(12, 'Pago de honorarios', '1080.00', '2024-11-02'),
(13, 'Pago de honorarios', '904.50', '2024-11-02'),
(14, 'Pago de honorarios', '1407.00', '2024-11-02'),
(15, 'Pago de honorarios', '2160.00', '2024-11-03'),
(16, 'Pago de honorarios', '2110.50', '2024-11-09'),
(18, 'Pago de honorarios', '1008.00', '2024-11-12'),
(19, 'Pago de honorarios', '2040.00', '2024-11-12'),
(20, 'Pago de honorarios', '1080.00', '2024-11-12'),
(21, 'Pago de honorarios', '2160.00', '2024-11-15');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id_Usuario` smallint(6) NOT NULL COMMENT 'id del usuario',
  `Nombre_Usuario` varchar(50) NOT NULL COMMENT 'Nombre',
  `IdRol_Usuario` smallint(6) NOT NULL COMMENT 'Id rol de usuario',
  `Pass_Usuario` varchar(100) NOT NULL COMMENT 'password del usuario',
  `dtFechaRegistro` date DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish2_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id_Usuario`, `Nombre_Usuario`, `IdRol_Usuario`, `Pass_Usuario`, `dtFechaRegistro`) VALUES
(7, 'Default', 1, 'U2FsdGVkX1/jxdOm8HZn2ZRSIYpqlfWFYQz8Oy4SBD0=', '2024-10-16'),
(9, 'adm45', 2, 'U2FsdGVkX19qHXbk6M1NAoqbdcaZHrZ38eYGpD8SoJM=', '2024-10-16'),
(13, 'bartolome', 2, 'U2FsdGVkX1+/vBB8zF2dRn2OGr9uLuZq2wK9XK4eUrA=', '2024-10-16');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `cattipoclientes`
--
ALTER TABLE `cattipoclientes`
  ADD PRIMARY KEY (`IdTipoCliente`);

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`Id_Cliente`);

--
-- Indices de la tabla `empleados`
--
ALTER TABLE `empleados`
  ADD PRIMARY KEY (`Id_Empleado`);

--
-- Indices de la tabla `entradasmembrecias`
--
ALTER TABLE `entradasmembrecias`
  ADD PRIMARY KEY (`Id_Entrada`);

--
-- Indices de la tabla `horaspagadas`
--
ALTER TABLE `horaspagadas`
  ADD PRIMARY KEY (`Id_HoraPagada`);

--
-- Indices de la tabla `horastrabajadas`
--
ALTER TABLE `horastrabajadas`
  ADD PRIMARY KEY (`Id_Horas`);

--
-- Indices de la tabla `membrecias`
--
ALTER TABLE `membrecias`
  ADD PRIMARY KEY (`Id_Membrecia`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id_Rol`);

--
-- Indices de la tabla `salidas`
--
ALTER TABLE `salidas`
  ADD PRIMARY KEY (`Id_Salida`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id_Usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `cattipoclientes`
--
ALTER TABLE `cattipoclientes`
  MODIFY `IdTipoCliente` smallint(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `clientes`
--
ALTER TABLE `clientes`
  MODIFY `Id_Cliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `empleados`
--
ALTER TABLE `empleados`
  MODIFY `Id_Empleado` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id del empleado', AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `entradasmembrecias`
--
ALTER TABLE `entradasmembrecias`
  MODIFY `Id_Entrada` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `horaspagadas`
--
ALTER TABLE `horaspagadas`
  MODIFY `Id_HoraPagada` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `horastrabajadas`
--
ALTER TABLE `horastrabajadas`
  MODIFY `Id_Horas` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT de la tabla `membrecias`
--
ALTER TABLE `membrecias`
  MODIFY `Id_Membrecia` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `Id_Rol` smallint(6) NOT NULL AUTO_INCREMENT COMMENT 'id del rol';

--
-- AUTO_INCREMENT de la tabla `salidas`
--
ALTER TABLE `salidas`
  MODIFY `Id_Salida` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id_Usuario` smallint(6) NOT NULL AUTO_INCREMENT COMMENT 'id del usuario', AUTO_INCREMENT=18;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

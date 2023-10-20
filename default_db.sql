-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 20-10-2023 a las 23:26:58
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `veterinaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `citas`
--

CREATE TABLE `citas` (
  `Id` int(11) NOT NULL,
  `Fecha` date NOT NULL,
  `Hora` time NOT NULL,
  `Motivo` longtext NOT NULL,
  `IdMascota` int(11) NOT NULL,
  `IdVeterinario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `citas`
--

INSERT INTO `citas` (`Id`, `Fecha`, `Hora`, `Motivo`, `IdMascota`, `IdVeterinario`) VALUES
(4, '2023-10-03', '10:20:00', 'Vacunación', 1, 1),
(6, '2022-10-03', '01:10:00', 'Vacunación', 1, 1),
(7, '2022-10-03', '19:10:00', 'General', 1, 1),
(8, '2022-10-03', '22:10:00', 'General', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `especies`
--

CREATE TABLE `especies` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `especies`
--

INSERT INTO `especies` (`Id`, `Nombre`) VALUES
(1, 'canina'),
(2, 'felina');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `laboratorios`
--

CREATE TABLE `laboratorios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Direccion` varchar(150) NOT NULL,
  `Telefono` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `laboratorios`
--

INSERT INTO `laboratorios` (`Id`, `Nombre`, `Direccion`, `Telefono`) VALUES
(1, 'Bayern', 'CRA 373 # 24-54', '3131231231'),
(2, 'P&G', 'CRA 1 # 10 - 11', '3123112313'),
(4, 'Genfar', 'CALLE MELA # 12-66', '313222222');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mascotas`
--

CREATE TABLE `mascotas` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `FechaNacimiento` datetime(6) NOT NULL,
  `IdPropietario` int(11) NOT NULL,
  `IdEspecie` int(11) NOT NULL,
  `IdRaza` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `mascotas`
--

INSERT INTO `mascotas` (`Id`, `Nombre`, `FechaNacimiento`, `IdPropietario`, `IdEspecie`, `IdRaza`) VALUES
(1, 'Tuerca', '2023-10-01 15:19:40.000000', 1, 1, 1),
(3, 'Martuchis', '2023-10-01 04:36:50.000000', 2, 2, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `medicamentomovimientos`
--

CREATE TABLE `medicamentomovimientos` (
  `Id` int(11) NOT NULL,
  `Cantidad` int(11) NOT NULL,
  `Fecha` datetime(6) NOT NULL,
  `PrecioUnitario` double NOT NULL,
  `IdMedicamento` int(11) NOT NULL,
  `IdTipoMovimiento` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `medicamentomovimientos`
--

INSERT INTO `medicamentomovimientos` (`Id`, `Cantidad`, `Fecha`, `PrecioUnitario`, `IdMedicamento`, `IdTipoMovimiento`) VALUES
(1, 1, '2023-10-20 00:01:20.000000', 3000, 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `medicamentos`
--

CREATE TABLE `medicamentos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Stock` int(11) NOT NULL,
  `Precio` double NOT NULL,
  `IdLaboratorio` int(11) NOT NULL,
  `IdProveedor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `medicamentos`
--

INSERT INTO `medicamentos` (`Id`, `Nombre`, `Stock`, `Precio`, `IdLaboratorio`, `IdProveedor`) VALUES
(1, 'Metanfetamina 250mg', 10, 2000, 1, 1),
(2, 'Perronidazol 250mg', 50, 50001, 1, 1),
(3, 'Acetaminofen Perruno 100mg', 1000, 70000, 4, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Email` varchar(150) NOT NULL,
  `Telefono` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id`, `Nombre`, `Email`, `Telefono`) VALUES
(1, 'Carlota', 'carlos@carlos.com', '3132476190'),
(2, 'Juca', 'juca@juca.com', '31071356378');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedores`
--

CREATE TABLE `proveedores` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Direccion` varchar(150) NOT NULL,
  `Telefono` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `proveedores`
--

INSERT INTO `proveedores` (`Id`, `Nombre`, `Direccion`, `Telefono`) VALUES
(1, 'Baguer S.A', 'CRA 20 # 28-28 ', '313222222');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `razas`
--

CREATE TABLE `razas` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `IdEspecie` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `razas`
--

INSERT INTO `razas` (`Id`, `Nombre`, `IdEspecie`) VALUES
(1, 'Golden Retriver', 1),
(2, 'Schanuser', 1),
(3, 'Angora', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `refreshtoken`
--

CREATE TABLE `refreshtoken` (
  `Id` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Token` longtext DEFAULT NULL,
  `FechaExpiracion` datetime(6) NOT NULL,
  `Created` datetime(6) NOT NULL,
  `Revoked` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `refreshtoken`
--

INSERT INTO `refreshtoken` (`Id`, `IdUsuario`, `Token`, `FechaExpiracion`, `Created`, `Revoked`) VALUES
(1, 1, 's4luK7yoc4TPuYx/IvYqlVClUuokqqViXI37vjCfrxE=', '2023-10-19 09:18:06.677764', '2023-10-19 09:15:06.677818', '2023-10-19 09:16:35.859450'),
(2, 1, 'pG1JtjnzHIx+KDUFEsSVUJBynOpB/M1LHUCZSuHEKcE=', '2023-10-19 09:19:35.859492', '2023-10-19 09:16:35.859492', NULL),
(3, 2, '6UseCOu6JSt3EsPOQOXNdLwQWcr0Sw54bcf+t6O0MLY=', '2023-10-20 15:52:09.102720', '2023-10-20 15:49:09.102770', NULL),
(4, 2, 'sRDg54mOI8qGZM6LxDFbJiT/8a/Qw+BScgmNr3niAJo=', '2023-10-20 16:14:13.185959', '2023-10-20 16:11:13.185959', NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`Id`, `Nombre`) VALUES
(3, 'Administrador'),
(1, 'WithoutRol');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipomovimientos`
--

CREATE TABLE `tipomovimientos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipomovimientos`
--

INSERT INTO `tipomovimientos` (`Id`, `Nombre`) VALUES
(2, 'Devolución Estándar'),
(1, 'Venta');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tratamientos`
--

CREATE TABLE `tratamientos` (
  `Id` int(11) NOT NULL,
  `Dosis` varchar(100) NOT NULL,
  `Administracion` datetime(6) NOT NULL,
  `Observaciones` varchar(100) NOT NULL,
  `IdCita` int(11) NOT NULL,
  `IdMedicamento` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tratamientos`
--

INSERT INTO `tratamientos` (`Id`, `Dosis`, `Administracion`, `Observaciones`, `IdCita`, `IdMedicamento`) VALUES
(1, 'Intervalo 8Hrs', '2023-10-20 00:47:57.000000', 'Debe insertarse la pastilla en la boca del animal.', 7, 1),
(2, 'Intervalo 10Hrs', '2023-10-20 00:47:57.000000', 'Debe insertarse la pastilla en la boca del canino.', 6, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuariorol`
--

CREATE TABLE `usuariorol` (
  `IdUsuario` int(11) NOT NULL,
  `IdRol` int(11) NOT NULL,
  `Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuariorol`
--

INSERT INTO `usuariorol` (`IdUsuario`, `IdRol`, `Id`) VALUES
(1, 1, 0),
(2, 3, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `DNI` varchar(15) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Username`, `DNI`, `email`, `password`) VALUES
(1, 'Adrian', '35263828', 'a@a.com', 'AQAAAAIAAYagAAAAEEzmR6oHlfHkVUIi9pNB471yJE0rgXY8dfCgQ0Ivo7N+LEVBUiiEhBmqsap9K/ODZA=='),
(2, 'admin', '0000000', 'admin@admin.com', 'AQAAAAIAAYagAAAAEMWnYe+OEB+9N7KdamawpVXbuSt6W6ty3JUPTwKBFsEopSYLLJZAj2e+7R3Xdegbew==');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `veterinarios`
--

CREATE TABLE `veterinarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Telefono` varchar(15) NOT NULL,
  `Especialidad` varchar(200) NOT NULL,
  `IdUsuario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `veterinarios`
--

INSERT INTO `veterinarios` (`Id`, `Nombre`, `Telefono`, `Especialidad`, `IdUsuario`) VALUES
(1, 'Adrián', '313222222', 'Cirujano Cardiovascular', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20231019034307_InitialMigration', '7.0.10');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `citas`
--
ALTER TABLE `citas`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Citas_IdMascota` (`IdMascota`),
  ADD KEY `IX_Citas_IdVeterinario` (`IdVeterinario`);

--
-- Indices de la tabla `especies`
--
ALTER TABLE `especies`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Especies_Nombre` (`Nombre`);

--
-- Indices de la tabla `laboratorios`
--
ALTER TABLE `laboratorios`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Laboratorios_Nombre` (`Nombre`);

--
-- Indices de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Mascotas_IdEspecie` (`IdEspecie`),
  ADD KEY `IX_Mascotas_IdPropietario` (`IdPropietario`),
  ADD KEY `IX_Mascotas_IdRaza` (`IdRaza`);

--
-- Indices de la tabla `medicamentomovimientos`
--
ALTER TABLE `medicamentomovimientos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_MedicamentoMovimientos_IdMedicamento` (`IdMedicamento`),
  ADD KEY `IX_MedicamentoMovimientos_IdTipoMovimiento` (`IdTipoMovimiento`);

--
-- Indices de la tabla `medicamentos`
--
ALTER TABLE `medicamentos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Medicamentos_IdLaboratorio` (`IdLaboratorio`),
  ADD KEY `IX_Medicamentos_IdProveedor` (`IdProveedor`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Propietarios_Email` (`Email`);

--
-- Indices de la tabla `proveedores`
--
ALTER TABLE `proveedores`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Proveedores_Nombre` (`Nombre`);

--
-- Indices de la tabla `razas`
--
ALTER TABLE `razas`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Razas_Nombre` (`Nombre`),
  ADD KEY `IX_Razas_IdEspecie` (`IdEspecie`);

--
-- Indices de la tabla `refreshtoken`
--
ALTER TABLE `refreshtoken`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_RefreshToken_IdUsuario` (`IdUsuario`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Roles_Nombre` (`Nombre`);

--
-- Indices de la tabla `tipomovimientos`
--
ALTER TABLE `tipomovimientos`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_TipoMovimientos_Nombre` (`Nombre`);

--
-- Indices de la tabla `tratamientos`
--
ALTER TABLE `tratamientos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Tratamientos_IdCita` (`IdCita`),
  ADD KEY `IX_Tratamientos_IdMedicamento` (`IdMedicamento`);

--
-- Indices de la tabla `usuariorol`
--
ALTER TABLE `usuariorol`
  ADD PRIMARY KEY (`IdUsuario`,`IdRol`),
  ADD KEY `IX_UsuarioRol_IdRol` (`IdRol`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Usuarios_DNI` (`DNI`);

--
-- Indices de la tabla `veterinarios`
--
ALTER TABLE `veterinarios`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Veterinarios_IdUsuario` (`IdUsuario`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `citas`
--
ALTER TABLE `citas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `especies`
--
ALTER TABLE `especies`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `laboratorios`
--
ALTER TABLE `laboratorios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `mascotas`
--
ALTER TABLE `mascotas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `medicamentomovimientos`
--
ALTER TABLE `medicamentomovimientos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `medicamentos`
--
ALTER TABLE `medicamentos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `proveedores`
--
ALTER TABLE `proveedores`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `razas`
--
ALTER TABLE `razas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `refreshtoken`
--
ALTER TABLE `refreshtoken`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `tipomovimientos`
--
ALTER TABLE `tipomovimientos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `tratamientos`
--
ALTER TABLE `tratamientos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `veterinarios`
--
ALTER TABLE `veterinarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `citas`
--
ALTER TABLE `citas`
  ADD CONSTRAINT `FK_Citas_Mascotas_IdMascota` FOREIGN KEY (`IdMascota`) REFERENCES `mascotas` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Citas_Veterinarios_IdVeterinario` FOREIGN KEY (`IdVeterinario`) REFERENCES `veterinarios` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `mascotas`
--
ALTER TABLE `mascotas`
  ADD CONSTRAINT `FK_Mascotas_Especies_IdEspecie` FOREIGN KEY (`IdEspecie`) REFERENCES `especies` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Mascotas_Propietarios_IdPropietario` FOREIGN KEY (`IdPropietario`) REFERENCES `propietarios` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Mascotas_Razas_IdRaza` FOREIGN KEY (`IdRaza`) REFERENCES `razas` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `medicamentomovimientos`
--
ALTER TABLE `medicamentomovimientos`
  ADD CONSTRAINT `FK_MedicamentoMovimientos_Medicamentos_IdMedicamento` FOREIGN KEY (`IdMedicamento`) REFERENCES `medicamentos` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_MedicamentoMovimientos_TipoMovimientos_IdTipoMovimiento` FOREIGN KEY (`IdTipoMovimiento`) REFERENCES `tipomovimientos` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `medicamentos`
--
ALTER TABLE `medicamentos`
  ADD CONSTRAINT `FK_Medicamentos_Laboratorios_IdLaboratorio` FOREIGN KEY (`IdLaboratorio`) REFERENCES `laboratorios` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Medicamentos_Proveedores_IdProveedor` FOREIGN KEY (`IdProveedor`) REFERENCES `proveedores` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `razas`
--
ALTER TABLE `razas`
  ADD CONSTRAINT `FK_Razas_Especies_IdEspecie` FOREIGN KEY (`IdEspecie`) REFERENCES `especies` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `refreshtoken`
--
ALTER TABLE `refreshtoken`
  ADD CONSTRAINT `FK_RefreshToken_Usuarios_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `tratamientos`
--
ALTER TABLE `tratamientos`
  ADD CONSTRAINT `FK_Tratamientos_Citas_IdCita` FOREIGN KEY (`IdCita`) REFERENCES `citas` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Tratamientos_Medicamentos_IdMedicamento` FOREIGN KEY (`IdMedicamento`) REFERENCES `medicamentos` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `usuariorol`
--
ALTER TABLE `usuariorol`
  ADD CONSTRAINT `FK_UsuarioRol_Roles_IdRol` FOREIGN KEY (`IdRol`) REFERENCES `roles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UsuarioRol_Usuarios_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`Id`) ON DELETE CASCADE;

--
-- Filtros para la tabla `veterinarios`
--
ALTER TABLE `veterinarios`
  ADD CONSTRAINT `FK_Veterinarios_Usuarios_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

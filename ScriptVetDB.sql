
USE VetDB;

/* INSERT PARA TIPOMASCOTAS */
INSERT INTO TipoMascotas(Nombre, Estado) VALUES ('Perro', 1);
INSERT INTO TipoMascotas(Nombre, Estado) VALUES ('Gato', 1);


/* INSERTS PARA RAZAS */
INSERT INTO RazaMascotas (IdTipoMascota, Nombre, Estado)
VALUES 
    (1, 'Labrador Retriever', 1),
	(1, 'Pitbull', 1),
	(2, 'Cimarron', 1),
    (2, 'Persa', 1);

/* INSERTS PARA Rol */
INSERT INTO Roles (Tipo, Estado)
VALUES 
    ('Admin', 1),
	('Cliente', 1),
	('Veterinario', 1);

/*INSERTS PARA USUARIOS*/
INSERT INTO Usuarios (IdRol, NombreUsuario, Contrasena, Imagen, UltimaFechaConexion, Estado) 
VALUES 
	(1, 'Jafet', 'Prueba123', 'ImagenPrueba', '2024-03-20 12:30:00', 1),
	(2, 'Alexa', 'Prueba123', 'ImagenPrueba', '2024-03-20 12:30:00', 1),
	(2, 'Violeta', 'Prueba123', 'ImagenPrueba', '2024-03-20 12:30:00', 1),
	(2, 'Jose', 'Prueba123', 'ImagenPrueba', '2024-03-20 12:30:00', 1),
	(3, 'Jesus', 'Prueba123', 'ImagenPrueba', '2024-03-20 12:30:00', 1),
	(3, 'Katherine', 'Prueba123', 'ImagenPrueba', '2024-03-20 12:30:00', 1);


	/*INSERTS PARA MASCOTA*/
 INSERT INTO Mascotas(Nombre, IdTipoMascota, IdRazaMascota, Genero, Edad, Peso, Imagen, IdUsuarioCreacion, IdUsuario, FechaCreacion, FechaModificacion, Estado) 
VALUES 
    ('Astrid', 1, 1, 'Hembra', 2, 10, 'ImagenPrueba', 2, 1, '2024-03-20 12:30:00', '2024-03-20 12:30:00', 1),
    ('Leon', 1, 2, 'Macho', 3, 15, 'ImagenPrueba', 2, 3, '2024-03-20 12:30:00', '2024-03-20 12:30:00', 1),
    ('Luna', 2, 3, 'Hembra', 4, 20, 'ImagenPrueba', 3, 3, '2024-03-20 12:30:00', '2024-03-20 12:30:00', 1),
    ('Lilith', 2, 4, 'Hembra', 5, 25, 'ImagenPrueba', 4, 3, '2024-03-20 12:30:00', '2024-03-20 12:30:00', 1);

	/*INSERTS PARA MEDICAMENTOS*/
INSERT INTO Medicamentos(Nombre, Marca, Estado) 
VALUES 
    ('Paracetamol', 'Tylenol', 1),
    ('Ibuprofeno', 'Advil', 1),
    ('Omeprazol', 'Prilosec', 1),
    ('Amoxicilina', 'Amoxil', 1);  


/*INSERTS PARA PADECIMIENTOS*/
INSERT INTO Padecimientos (Nombre, Estado) 
VALUES 
    ('Dermatitis', 1),
    ('Otitis', 1),
    ('Artritis', 1),
    ('Gastritis', 1);

INSERT INTO Vacunas (Nombre, TipoVacuna, Producto, Estado) 
VALUES 
    ('Rabia', 'Prevención', 'Vacuna A', 1),
    ('Parvovirus', 'Prevención', 'Vacuna B', 1),
    ('Moquillo', 'Prevención', 'Vacuna C', 1),
    ('Leptospirosis', 'Prevención', 'Vacuna D', 1);


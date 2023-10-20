:shipit: Veterinaria
==================================================

Este repositorio contiene una APP ASP.NET llamada Veterinaria que busca cumplir una serie de requerimientos.

🎱 Requerimientos
-------------------

#### Endpoints requeridos
1. Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular. ✔️
2. Listar los medicamentos que pertenezcan a el laboratorio Genfar ✔️
3. Mostrar las mascotas que se encuentren registradas cuya especie sea felina. ✔️
4. Listar los propietarios y sus mascotas. ✔️
5. Listar los medicamentos que tenga un precio de venta mayor a 50000 ✔️
6. Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
7. Listar todas las mascotas agrupadas por especie. ✔️
8. Listar todos los movimientos de medicamentos y el valor total de cada movimiento. 
9. Listar las mascotas que fueron atendidas por un determinado veterinario. ✔️
10. Listar los proveedores que me venden un determinado medicamento.
11. Listar las mascotas y sus propietarios cuya raza sea Golden Retriver
12. Listar la cantidad de mascotas que pertenecen a una raza a una raza. Nota: Se debe mostrar una lista de las razas y la cantidad de mascotas que pertenecen a la raza.

🔧 Instalación
-------------------
1. Clonar el repo
2. Importar default_db.sql a nuestro MySQL usando cualquier gestor de bases de datos.
3. Configurar la conexión en ´appsettings.json´ (NO cambiar nombre de database)
4. dotnet run 🚀☘️

## API Reference 🛰️
Antes de poder hacer consultas debemos loguearnos.
```
    POST api/usuario/login
    {
      "Username": "admin",
      "Password": "admin"
    }
```
Usaremos este token para todas las consultas.

Esta app implementa un sistema estándar de CRUD para todas las entidades:
```
  GET api/{controlador}/listar
```
```
  POST api/{controlador}/crear
```
```
  PUT api/{controlador}/actualizar/{id}
```
```
  DELETE api/{controlador}/eliminar/{id}
```
NOTA** Debemos tener en cuenta que, al momento de CREAR y ACTUALIZAR, debemos devolver una estructura idéntica a la obtenida cuando listamos.

### ENDPOINTS ESPECÍFICOS
1. Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular.
NOTA** Los espacios se definen con '%' en la URL
```
  GET api/veterinario/listarXEspecialidad/{especialidad}
  GET api/veterinario/listarXEspecialidad/Cirujano%Cardiovascular
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/6a94d891-dbc0-438e-9ba9-347ae34031cd)

2. Listar los medicamentos que pertenezcan a el laboratorio Genfar
```
  GET api/medicamento/listarPorLaboratorio/{Laboratorio}
  GET api/medicamento/listarPorLaboratorio/Genfar
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/f314e215-09ad-439a-baca-e6c92c97750c)

3. Mostrar las mascotas que se encuentren registradas cuya especie sea felina.
```
  GET api/mascota/listarPorEspecie/{especie}
  GET api/mascota/listarPorEspecie/felina
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/2d3e1bb6-52f3-479f-a591-4f78cf1d86e6)

4. Listar los propietarios y sus mascotas.
```
  GET api/propietario/listarPropsConMascotas
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/8935ce7f-e656-4001-8c20-440aa6eca8c9)


5. Listar los medicamentos que tenga un precio de venta mayor a 50000
```
  GET api/medicamento/listarPrecioMayorA/{precio}
  GET api/medicamento/listarPrecioMayorA/50000
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/882b3772-e9bb-4f54-9387-a2c3660d353c)

7. Listar todas las mascotas agrupadas por especie.
```
  GET api/mascota/listarAgrupacionPorEspecie
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/4f3997ad-623b-4487-8596-55eb7764a727)

9. Listar las mascotas que fueron atendidas por un determinado veterinario.
```
  GET api/mascota/listarXVeterinario/{IdVeterinario}
```
![image](https://github.com/adrianAraqueG/dotnet_veterinaria/assets/79146629/865169fb-d3cd-4ddc-99e7-6ca7a9c9e62f)
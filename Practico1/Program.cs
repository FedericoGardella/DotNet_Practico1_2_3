using DAL;
using DAL.DALs;
using DAL.IDALs;
using DAL.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

IDAL_Personas dal = new DAL_Personas_ADONET();

string comando = "NUEVA";

Console.WriteLine("Bienvenido a mi primera app .NET!!!");


do
{
    Console.WriteLine("Ingrese comando (NUEVA/IMPRIMIR/ACTUALIZAR/ELIMINAR/EXIT): ");

    try
    {
        comando = Console.ReadLine().ToUpper();

        switch (comando)
        {
            case "NUEVA":
                Persona nuevaPersona = new Persona();
                Console.WriteLine("Ingrese el nombre de la persona: ");
                nuevaPersona.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el documento de la persona: ");
                nuevaPersona.Documento = Console.ReadLine();
                dal.AddPersona(nuevaPersona);
                Console.WriteLine("Persona agregada exitosamente.");
                break;

            case "IMPRIMIR":
                Console.WriteLine("Ingrese Nombre o Documento:");
                string filtro = Console.ReadLine();

                List<Persona> filtradas = dal.GetPersonas()
                    .Where(p => p.Nombre.Contains(filtro) || p.Documento.Contains(filtro))
                    .OrderBy(p => p.Nombre)
                    .ToList();

                foreach (Persona p in filtradas)
                    Console.WriteLine(p.GetString());
                break;

            case "ACTUALIZAR":
                Console.WriteLine("Ingrese el Id de la persona a actualizar: ");
                if (int.TryParse(Console.ReadLine(), out int idActualizar))
                {
                    Persona personaActualizar = dal.GetPersona(idActualizar);
                    if (personaActualizar != null)
                    {
                        Console.WriteLine($"Nombre actual: {personaActualizar.Nombre}. Ingrese el nuevo nombre: ");
                        personaActualizar.Nombre = Console.ReadLine();
                        Console.WriteLine($"Documento actual: {personaActualizar.Documento}. Ingrese el nuevo documento: ");
                        personaActualizar.Documento = Console.ReadLine();
                        dal.UpdatePersona(personaActualizar);
                        Console.WriteLine("Persona actualizada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Persona no encontrada.");
                    }
                }
                else
                {
                    Console.WriteLine("Id inválido.");
                }
                break;

            case "ELIMINAR":
                Console.WriteLine("Ingrese el Id de la persona a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out int idEliminar))
                {
                    Persona personaEliminar = dal.GetPersona(idEliminar);
                    if (personaEliminar != null)
                    {
                        dal.DeletePersona(idEliminar);
                        Console.WriteLine("Persona eliminada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Persona no encontrada.");
                    }
                }
                else
                {
                    Console.WriteLine("Id inválido.");
                }
                break;

            case "EXIT":
                break;

            default:
                Console.WriteLine("Comando no reconocido.");
                break;


        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ocurrió un error: " + ex.Message);
    }
}
while (comando != "EXIT");


//Luego de "EXIT" se ejecuta automaticamente una prueba con Entity Framework


using (var context = new DBContext())
{
    // Crear una instancia del repositorio
    var dalPersonas = new DAL_Personas_EF(context);

    // Agregar una nueva persona
    var nuevaPersona = new Persona
    {
        Nombre = "Juano Pérez",
        Documento = "12344678"
    };
    dalPersonas.AddPersona(nuevaPersona);
    Console.WriteLine("Persona agregada exitosamente.");

    // Leer todas las personas
    var personas = dalPersonas.GetPersonas();
    Console.WriteLine("Lista de Personas:");
    foreach (var persona in personas)
    {
        Console.WriteLine($"ID: {persona.Id}, Nombre: {persona.Nombre}, Documento: {persona.Documento}");
    }

    // Actualizar una persona
    var personaActualizada = personas.FirstOrDefault();
    if (personaActualizada != null)
    {
        personaActualizada.Nombre = "Juan Carlos Pérez";
        personaActualizada.Documento = "87654321";
        dalPersonas.UpdatePersona(personaActualizada);
        Console.WriteLine($"Persona con ID {personaActualizada.Id} actualizada exitosamente.");
    }

    // Eliminar una persona por ID
    if (personaActualizada != null)
    {
        dalPersonas.DeletePersona(personaActualizada.Id);
        Console.WriteLine($"Persona con ID {personaActualizada.Id} eliminada exitosamente.");
    }
}

using (var context = new DBContext())
{
    // Crear una instancia del repositorio de vehículos
    var dalVehiculos = new DAL_Vehiculos_EF(context);

    // Agregar un nuevo vehículo
    var nuevoVehiculo = new Vehiculo
    {
        Marca = "Toyota",
        Modelo = "Corolla",
        Matricula = "ABC123",
        PersonaId = 1 // Suponiendo que existe una persona con ID 1
    };
    dalVehiculos.AddVehiculo(nuevoVehiculo);
    Console.WriteLine("Vehículo agregado exitosamente.");

    // Leer todos los vehículos
    var vehiculos = dalVehiculos.GetVehiculos();
    Console.WriteLine("Lista de Vehículos:");
    foreach (var vehiculo in vehiculos)
    {
        Console.WriteLine($"ID: {vehiculo.Id}, Marca: {vehiculo.Marca}, Modelo: {vehiculo.Modelo}, Matrícula: {vehiculo.Matricula}, PersonaId: {vehiculo.PersonaId}");
    }

    // Actualizar un vehículo
    var vehiculoActualizado = vehiculos.FirstOrDefault();
    if (vehiculoActualizado != null)
    {
        vehiculoActualizado.Marca = "Honda";
        vehiculoActualizado.Modelo = "Civic";
        vehiculoActualizado.Matricula = "XYZ987";
        dalVehiculos.UpdateVehiculo(vehiculoActualizado);
        Console.WriteLine($"Vehículo con ID {vehiculoActualizado.Id} actualizado exitosamente.");
    }

    // Eliminar un vehículo por ID
    if (vehiculoActualizado != null)
    {
        dalVehiculos.DeleteVehiculo(vehiculoActualizado.Id);
        Console.WriteLine($"Vehículo con ID {vehiculoActualizado.Id} eliminado exitosamente.");
    }
}
//DUDAS:

//Preguntarle a Cristian por que carajo esto funciona si no tengo el DAL_Vehiculos_EF.cs

//Estructura de proyectos y carpetas probablemente mal

//Tuve que poner DBContect en public

//No termino de entender si estoy usando Lambda Queries o LINQ o ninguno de los dos

//Desentendimiento general en como se componen las aplicaciones, parece ser un MVC pero no

//Cada vez que me conecto a la BDD me aparece esto: Advertencia de seguridad: el elemento TLS 1.0 negociado es un protocolo inseguro y
//solo se admite por compatibilidad con versiones anteriores. La versión recomendada del protocolo es TLS 1.2 y versiones posteriores.

//Necesito entender mejor cuando tengo que poner "Personas" en plural y "Persona" en singular

Console.WriteLine("Hasta luego!!!");
Console.ReadLine();

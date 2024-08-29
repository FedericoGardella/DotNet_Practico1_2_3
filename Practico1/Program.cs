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
    // Crear una nueva persona
    var nuevaPersona = new Personas
    {
        Nombre = "Juan Perez",
        Documento = "12345678"
    };

    context.Personas.Add(nuevaPersona);

    context.SaveChanges();

    Console.WriteLine("Persona agregada exitosamente.");
}

using (var context = new DBContext())
{
    var persona = context.Personas.FirstOrDefault(p => p.Id == 1);
    if (persona != null)
    {
        // Crear un nuevo vehículo asociado a la persona
        var nuevoVehiculo = new Vehiculos
        {
            Marca = "Toyota",
            Modelo = "Corolla",
            Matricula = "ABC-123",
            PersonaId = persona.Id
        };

        context.Vehiculos.Add(nuevoVehiculo);

        context.SaveChanges();

        Console.WriteLine("Vehículo agregado exitosamente.");
    }
    else
    {
        Console.WriteLine("No se encontró la persona.");
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



Console.WriteLine("Hasta luego!!!");
Console.ReadLine();

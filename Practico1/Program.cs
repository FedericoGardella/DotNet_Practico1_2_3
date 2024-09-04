using DAL;
using DAL.DALs;
using DAL.IDALs;
using Shared;
using System.Collections.Generic;


var context = new DBContext();
IDAL_Personas dalPersonas = new DAL_Personas_EF(context);
IDAL_Vehiculos dalVehiculos = new DAL_Vehiculos_EF(context);

string comando = "";

Console.WriteLine("Bienvenido a mi primera app .NET!!!");


do
{
    Console.WriteLine("Ingrese comando (NUEVA P o V/ IMPRIMIR P o V /ACTUALIZAR/ELIMINAR/EXIT): ");

    try
    {
        comando = Console.ReadLine().ToUpper();



        switch (comando)
        {
            case "NUEVA P":
                Persona nuevaPersona = new Persona();
                Console.WriteLine("Ingrese el nombre de la persona: ");
                nuevaPersona.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el documento de la persona: ");
                nuevaPersona.Documento = Console.ReadLine();
                dalPersonas.AddPersona(nuevaPersona);
                Console.WriteLine("Persona agregada exitosamente.");
                break;

            case "IMPRIMIR P":
                Console.WriteLine("Ingrese Nombre o Documento:");
                string filtro = Console.ReadLine();

                List<Persona> filtradas = dalPersonas.GetPersonas()
                    .Where(p => p.Nombre.Contains(filtro) || p.Documento.Contains(filtro))
                    .OrderBy(p => p.Nombre)
                    .ToList();

                foreach (Persona p in filtradas)
                    Console.WriteLine(p.GetString());
                break;

            case "ACTUALIZAR P":
                Console.WriteLine("Ingrese el Id de la persona a actualizar: ");
                if (int.TryParse(Console.ReadLine(), out int idActualizar))
                {
                    Persona personaActualizar = dalPersonas.GetPersona(idActualizar);
                    if (personaActualizar != null)
                    {
                        Console.WriteLine($"Nombre actual: {personaActualizar.Nombre}. Ingrese el nuevo nombre: ");
                        personaActualizar.Nombre = Console.ReadLine();
                        Console.WriteLine($"Documento actual: {personaActualizar.Documento}. Ingrese el nuevo documento: ");
                        personaActualizar.Documento = Console.ReadLine();
                        dalPersonas.UpdatePersona(personaActualizar);
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

            case "ELIMINAR P":
                Console.WriteLine("Ingrese el Id de la persona a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out int idEliminar))
                {
                    Persona personaEliminar = dalPersonas.GetPersona(idEliminar);
                    if (personaEliminar != null)
                    {
                        dalPersonas.DeletePersona(idEliminar);
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
            case "NUEVO V":

                Vehiculo nuevoVehiculo = new Vehiculo();
                Console.WriteLine("Ingrese la marca: ");
                nuevoVehiculo.Marca = Console.ReadLine();
                Console.WriteLine("Ingrese el modelo: ");
                nuevoVehiculo.Modelo = Console.ReadLine();
                Console.WriteLine("Ingrese la matricula: ");
                nuevoVehiculo.Matricula = Console.ReadLine();

                List<Persona> personas = dalPersonas.GetPersonas()
                .OrderBy(p => p.Nombre)
                .ToList();

                foreach (Persona p in personas)
                    Console.WriteLine(p.GetString());

                Console.WriteLine("Ingrese el id del propietario: ");
                nuevoVehiculo.PersonaId = long.Parse(Console.ReadLine());


                dalVehiculos.AddVehiculo(nuevoVehiculo);
                Console.WriteLine("Vehículo agregado exitosamente.");


                break;

            case "IMPRIMIR V":

                // Leer todos los vehículos
                List <Vehiculo> vehiculos = dalVehiculos.GetVehiculos();
                Console.WriteLine("Lista de Vehículos:");
                foreach (Vehiculo vehiculo in vehiculos)
                {
                    Console.WriteLine($"ID: {vehiculo.Id}, Marca: {vehiculo.Marca}, Modelo: {vehiculo.Modelo}, Matrícula: {vehiculo.Matricula}, PersonaId: {vehiculo.PersonaId}");
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

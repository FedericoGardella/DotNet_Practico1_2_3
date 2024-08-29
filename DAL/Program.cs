using System;
using DAL;
using DAL.Models;

class Program
{
    static void Main(string[] args)
    {
        // Crear una instancia del DBContext
        using (var context = new DBContext())
        {
            // Crear una nueva persona
            var nuevaPersona = new Personas
            {
                Nombre = "Juan Perez",
                Documento = "12345678"
            };

            // Agregar la persona al contexto
            context.Personas.Add(nuevaPersona);

            // Guardar cambios en la base de datos
            context.SaveChanges();

            Console.WriteLine("Persona agregada exitosamente.");
        }
    }
}

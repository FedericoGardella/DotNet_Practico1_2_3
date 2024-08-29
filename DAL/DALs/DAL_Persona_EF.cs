using DAL.IDALs;
using DAL.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class DAL_Personas_EF : IDAL_Personas
    {
        private readonly DBContext _context;

        public DAL_Personas_EF(DBContext context)
        {
            _context = context;
        }

        public List<Persona> GetPersonas()
        {
            return _context.Personas
                .Select(p => new Persona
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Documento = p.Documento
                })
                .ToList();
        }

        public Persona GetPersona(long id)
        {
            return _context.Personas
                .Where(p => p.Id == id)
                .Select(p => new Persona
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Documento = p.Documento
                })
                .FirstOrDefault();
        }

        public void AddPersona(Persona persona)
        {
            var personaEntity = new Personas
            {
                Nombre = persona.Nombre,
                Documento = persona.Documento
            };

            _context.Personas.Add(personaEntity);
            _context.SaveChanges();
        }

        public void DeletePersona(long id)
        {
            var persona = _context.Personas.Find(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                _context.SaveChanges();
            }
        }

        public void UpdatePersona(Persona persona)
        {
            var personaEntity = _context.Personas.Find(persona.Id);
            if (personaEntity != null)
            {
                personaEntity.Nombre = persona.Nombre;
                personaEntity.Documento = persona.Documento;

                _context.SaveChanges();
            }
        }
    }
}

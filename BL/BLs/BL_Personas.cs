using BL.IBLs;
using DAL.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLs
{
    public class BL_Personas : IBL_Personas
    {
        public readonly IDAL_Personas _dal;

        public BL_Personas(IDAL_Personas dal)
        {
            _dal = dal; 
        }

        public List<Persona> GetPersonas()
        {
            return _dal.GetPersonas();
        }
        public Persona GetPersona(long id)
        {
            return _dal.GetPersona(id);
        }
        public void AddPersona(Persona persona)
        {
            _dal.AddPersona(persona);
        }
        public void DeletePersona(long id)
        {
            _dal.DeletePersona(id);
        }
        public void UpdatePersona(Persona persona)
        {
            _dal.UpdatePersona(persona);
        }

    }
}

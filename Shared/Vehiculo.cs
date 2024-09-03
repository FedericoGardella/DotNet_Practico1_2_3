using System;

namespace Shared
{
    public class Vehiculo
    {
        public long Id { get; set; }

        private string marca = "-- Sin Marca --";
        public string Marca
        {
            get { return marca; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("La marca no puede estar vacía.");
                else
                    marca = value;
            }
        }

        private string modelo = "-- Sin Modelo --";
        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("El modelo no puede estar vacío.");
                else
                    modelo = value;
            }
        }

        private string matricula = "";
        public string Matricula
        {
            get { return matricula; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                    throw new Exception("Formato de matrícula incorrecto.");
                else
                    matricula = value.ToUpper();
            }
        }

        public long PersonaId { get; set; }

        public string GetString()
        {
            return $"Id: {Id}, Marca: {Marca}, Modelo: {Modelo}, Matrícula: {Matricula}, PersonaId: {PersonaId}";
        }
    }
}


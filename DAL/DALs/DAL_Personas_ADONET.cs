using DAL.IDALs;
using Microsoft.Data.SqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.DALs
{
    public class DAL_Personas_ADONET : IDAL_Personas
    {
        private readonly string _connectionString = "Server=LAPTOP-HR0OQV62\\SQLEXPRESS;Database=Practico2;User Id=sa;Password=1234; TrustServerCertificate=True";

        public DAL_Personas_ADONET()
        {
        }

        public List<Persona> GetPersonas()
        {
            List<Persona> personas = new List<Persona>();

            string query = "SELECT Id, Nombre, Documento FROM Personas";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Persona persona = new Persona
                            {
                                Id = reader.GetInt64(0),
                                Nombre = reader["Nombre"].ToString(),
                                Documento = reader["Documento"].ToString()
                            };

                            personas.Add(persona);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
            }

            return personas;
        }


        public Persona GetPersona(long id)
        {
            Persona persona = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Personas WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            persona = new Persona
                            {
                                Id = reader.GetInt64(0),
                                Nombre = reader.GetString(1),
                                Documento = reader.GetString(2),
                            };
                        }
                    }
                }
            }
            return persona;
        }


        public void AddPersona(Persona persona)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Personas (Nombre, Documento) VALUES (@Nombre, @Documento) ", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@Documento", persona.Documento);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePersona(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Personas WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePersona(Persona persona)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Personas SET Nombre = @Nombre WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", persona.Id);
                    command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@Documento", persona.Documento);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

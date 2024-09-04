using DAL.IDALs;
using DAL.Models;
using Shared;


namespace DAL
{
    public class DAL_Vehiculos_EF : IDAL_Vehiculos
    {
        private readonly DBContext _context;

        public DAL_Vehiculos_EF(DBContext context)
        {
            _context = context;
        }

        // Obtener todos los vehículos
        public List<Vehiculo> GetVehiculos()
        {
            return _context.Vehiculos
                .Select(v => new Vehiculo
                {
                    Id = v.Id,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Matricula = v.Matricula,
                    PersonaId = v.PersonaId
                })
                .ToList();
        }

        // Obtener un vehículo por ID
        public Vehiculo GetVehiculo(long id)
        {
            return _context.Vehiculos
                .Where(v => v.Id == id)
                .Select(v => new Vehiculo
                {
                    Id = v.Id,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Matricula = v.Matricula,
                    PersonaId = v.PersonaId
                })
                .FirstOrDefault();
        }

        // Agregar un nuevo vehículo
        public void AddVehiculo(Vehiculo vehiculo)
        {
            var vehiculoEntity = new Vehiculos
            {
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Matricula = vehiculo.Matricula,
                PersonaId = vehiculo.PersonaId
            };

            _context.Vehiculos.Add(vehiculoEntity);
            _context.SaveChanges();
        }

        // Eliminar un vehículo por ID
        public void DeleteVehiculo(long id)
        {
            var vehiculo = _context.Vehiculos.Find(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                _context.SaveChanges();
            }
        }

        // Actualizar un vehículo
        public void UpdateVehiculo(Vehiculo vehiculo)
        {
            var vehiculoEntity = _context.Vehiculos.Find(vehiculo.Id);
            if (vehiculoEntity != null)
            {
                vehiculoEntity.Marca = vehiculo.Marca;
                vehiculoEntity.Modelo = vehiculo.Modelo;
                vehiculoEntity.Matricula = vehiculo.Matricula;
                vehiculoEntity.PersonaId = vehiculo.PersonaId;

                _context.SaveChanges();
            }
        }
    }
}

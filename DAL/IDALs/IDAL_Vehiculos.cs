using DAL.Models;
using Shared;
using System.Collections.Generic;

namespace DAL.IDALs
{
    public interface IDAL_Vehiculos
    {
        List<Vehiculo> GetVehiculos();
        Vehiculo GetVehiculo(long id);
        void AddVehiculo(Vehiculo vehiculo);
        void DeleteVehiculo(long id);
        void UpdateVehiculo(Vehiculo vehiculo);
    }
}

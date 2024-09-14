using DAL.IDALs;
using BL.IBLs;
using DAL.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BLs
{
    public class BL_Vehiculos : IBL_Vehiculos
    {

        private IDAL_Vehiculos _dalVehi;

        public BL_Vehiculos(IDAL_Vehiculos _dal)
        {
            _dalVehi = _dal;
        }

        public void AddVehiculo(Vehiculo vehiculo)
        {
            _dalVehi.AddVehiculo(vehiculo);
        }

        public Vehiculo GetVehiculo(long id)
        {
            return _dalVehi.GetVehiculo(id);
        }
        public List<Vehiculo> GetVehiculos()
        {
            return _dalVehi.GetVehiculos();
        }

        public void DeleteVehiculo(long id)
        {
            _dalVehi.DeleteVehiculo(id);
        }

        public void UpdateVehiculo(Vehiculo vehiculo)
        {
            _dalVehi.UpdateVehiculo(vehiculo);
        }

        public List<Vehiculo> GetVehiculosPorPropietario(long propietarioId)
        {
            return _dalVehi.GetVehiculosPorPropietario(propietarioId);
        }

    }
}

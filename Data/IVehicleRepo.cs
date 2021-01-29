using System.Collections.Generic;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public interface IVehicleRepo
    {
        bool SaveChanges();
        IEnumerable<Vehicle> GetAllVehicles();
        void CreateVehicle(Vehicle vehicle);
        Vehicle GetVehicleByPlate(string plate);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
    }
}
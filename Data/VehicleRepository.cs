using System;
using System.Collections.Generic;
using System.Linq;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public class VehicleRepository : IVehicleRepo
    {
        private readonly DataContext context;

        public VehicleRepository(DataContext context)
        {
            this.context = context;
        }
        public void CreateVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            this.context.Vehicles.Add(vehicle);
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            this.context.Vehicles.Remove(vehicle);
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return this.context.Vehicles.ToList();
        }

        public Vehicle GetVehicleByPlate(string plate)
        {
            return this.context.Vehicles.FirstOrDefault(v => v.Plate.Equals(plate));
        }

        public bool SaveChanges()
        {
            return (this.context.SaveChanges() >= 0);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            //Nothing
        }
    }
}
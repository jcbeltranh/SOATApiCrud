using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public class SoatRepository : ISoatRepo
    {
        private readonly DataContext context;

        public SoatRepository(DataContext context)
        {
            this.context = context;
        }
        public void CreateSOAT(SOAT soat)
        {
            if (soat == null)
                throw new ArgumentNullException(nameof(soat));
            soat.Owner = this.context.Users.FirstOrDefault(u => u.Document == soat.Owner.Document);
            soat.Vehicle = this.context.Vehicles.FirstOrDefault(u => u.Plate.Equals(soat.Vehicle.Plate));
            this.context.SOATs.Add(soat);
        }

        public void DeleteSOAT(SOAT soat)
        {
            if (soat == null)
                throw new ArgumentNullException(nameof(soat));
            this.context.SOATs.Remove(soat);
        }

        public IEnumerable<SOAT> GetAllSoats()
        {
            return this.context.SOATs
                .Include(soat => soat.Owner)
                .Include(soat => soat.Vehicle)
                .ToList();
        }

        public IEnumerable<SOAT> GetSOATByOwnerDocumentAndPlate(int document, string plate)
        {
            return this.context.SOATs
                .Where(s => s.Owner.Document == document && s.Vehicle.Plate.Equals(plate))
                .Include(soat => soat.Owner)
                .Include(soat => soat.Vehicle)
                .ToList();
        }

        public SOAT GetSOATByOwnerDocumentPlateAndYear(int document, string plate, DateTime year)
        {
            return this.context.SOATs
                .Where(s => s.Owner.Document == document && s.Vehicle.Plate.Equals(plate) && s.Year.Equals(year))
                .Include(soat => soat.Owner)
                .Include(soat => soat.Vehicle)
                .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (this.context.SaveChanges() >= 0);
        }

        public void UpdateSOAT(SOAT soat)
        {
            //Nothing
        }
    }
}
using System;
using System.Collections.Generic;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public interface ISoatRepo
    {
        bool SaveChanges();
        IEnumerable<SOAT> GetAllSoats();
        void CreateSOAT(SOAT soat);
        IEnumerable<SOAT> GetSOATByOwnerDocumentAndPlate(int document, string plate);
        SOAT GetSOATByOwnerDocumentPlateAndYear(int document, string plate, DateTime year);
        void UpdateSOAT(SOAT soat);
        void DeleteSOAT(SOAT soat);
        
    }
}
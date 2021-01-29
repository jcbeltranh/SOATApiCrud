using System;
using System.Collections.Generic;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public class MockSoatRepo : ISoatRepo
    {
        public void CreateSOAT(SOAT soat)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSOAT(SOAT soat)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SOAT> GetAllSoats()
        {
            return new List<SOAT>(){
                new SOAT(){
                    Owner = new User() { Document = 102204865, DocumentType = DocumentType.CC, Genre = "F", Name = "Viviana", Surname = "Avila Roa"},
                    Vehicle = new Vehicle() { Plate = "DFG 154", Axles = 2, Color = "Verde", Engine = "500 CC"},
                    Year = new DateTime(2020, 1, 1)
                },
                new SOAT(){
                    Owner = new User() { Document = 1019049865, DocumentType = DocumentType.CC, Genre = "M", Name = "Camilo", Surname = "Beltran Herrera"},
                    Vehicle = new Vehicle() { Plate = "SDA 154", Axles = 2, Color = "Rojo", Engine = "250 CC"},
                    Year = new DateTime(2019, 1, 1)
                },
                new SOAT(){
                    Owner = new User() { Document = 103254548, DocumentType = DocumentType.TI, Genre = "F", Name = "Lourdes", Surname = "Herrera Peña"},
                    Vehicle = new Vehicle() { Plate = "VFY 674", Axles = 2, Color = "Blanco", Engine = "700 CC"},
                    Year = new DateTime(2020, 1, 1)
                },
            };
        }

        public IEnumerable<SOAT> GetSOATByOwnerDocumentAndPlate(int document, string plate)
        {
            return new List<SOAT>(){
                new SOAT(){
                    Owner = new User() { Document = document, DocumentType = DocumentType.CC, Genre = "M", Name = "Camilo", Surname = "Beltran Herrera"},
                    Vehicle = new Vehicle() { Plate = plate, Axles = 2, Color = "Rojo", Engine = "250 CC"},
                    Year = new DateTime(2020, 1, 1)
                },
                new SOAT(){
                    Owner = new User() { Document = document, DocumentType = DocumentType.CC, Genre = "M", Name = "Camilo", Surname = "Beltran Herrera"},
                    Vehicle = new Vehicle() { Plate = plate, Axles = 2, Color = "Rojo", Engine = "250 CC"},
                    Year = new DateTime(2019, 1, 1)
                }
            };
        }

        public SOAT GetSOATByOwnerDocumentPlateAndYear(int document, string plate, DateTime year)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSOAT(SOAT soat)
        {
            throw new System.NotImplementedException();
        }
    }
}
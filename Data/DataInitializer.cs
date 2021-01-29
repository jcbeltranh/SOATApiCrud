using System;
using System.Linq;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public static class DataInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if(context.Users.Any())
            {
                return;
            }

            var Users = new User[]
            {
                new User(){Document=1019049848, Name="Juan Camilo", Surname="Beltran Herrera", DocumentType=DocumentType.CC, Genre="M"},
                new User(){Document=1022458798, Name="Alan David", Surname="Avila", DocumentType=DocumentType.TI, Genre="M"},
                new User(){Document=1019145325, Name="Esteban Camilo", Surname="Rodriguez", DocumentType=DocumentType.CC, Genre="M"},
                new User(){Document=28787024, Name="Lourdes", Surname="Herrera", DocumentType=DocumentType.NIT, Genre="F"},
                new User(){Document=101945865, Name="Breyner", Surname="Garzón Peña", DocumentType=DocumentType.DIP, Genre="M"},
                new User(){Document=102215743, Name="Viviana", Surname="Avila", DocumentType=DocumentType.DIP, Genre="F"},
            };

            context.Users.AddRange(Users);
            context.SaveChanges();

            var Vehicles = new Vehicle[]
            {
                new Vehicle() {Plate="AXF152", Axles=2, Color="Rojo", Engine="2000CC"},
                new Vehicle() {Plate="BJS452", Axles=2, Color="Blanco", Engine="2400CC"},
                new Vehicle() {Plate="KJ4548", Axles=2, Color="Negro", Engine="2600CC"},
                new Vehicle() {Plate="LKE154", Axles=3, Color="Verde", Engine="1600CC"},
                new Vehicle() {Plate="KPD154", Axles=2, Color="Gris", Engine="2200CC"},
                new Vehicle() {Plate="AHG152", Axles=2, Color="Negro", Engine="2000CC"},
                new Vehicle() {Plate="BK4541", Axles=3, Color="Azul", Engine="2000CC"},
            };

            context.Vehicles.AddRange(Vehicles);
            context.SaveChanges();
            
            var Soats = new SOAT[]
            {
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Alan")), Vehicle = Vehicles.First(v => v.Plate.Equals("AXF152")), Year=new DateTime(2018, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Juan")), Vehicle = Vehicles.First(v => v.Plate.Equals("BJS452")), Year=new DateTime(2017, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Alan")), Vehicle = Vehicles.First(v => v.Plate.Equals("AXF152")), Year=new DateTime(2019, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Esteban")), Vehicle = Vehicles.First(v => v.Plate.Equals("LKE154")), Year=new DateTime(2017, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Lourdes")), Vehicle = Vehicles.First(v => v.Plate.Equals("BK4541")), Year=new DateTime(2019, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Lourdes")), Vehicle = Vehicles.First(v => v.Plate.Equals("BK4541")), Year=new DateTime(2020, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Juan")), Vehicle = Vehicles.First(v => v.Plate.Equals("AHG152")), Year=new DateTime(2020, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Breyner")), Vehicle = Vehicles.First(v => v.Plate.Equals("KPD154")), Year=new DateTime(2018, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Breyner")), Vehicle = Vehicles.First(v => v.Plate.Equals("KPD154")), Year=new DateTime(2019, 1, 1)},
                new SOAT() {Owner = Users.First(u => u.Name.Contains("Viviana")), Vehicle = Vehicles.First(v => v.Plate.Equals("KJ4548")), Year=new DateTime(2020, 1, 1)},
            };

            foreach (var s in Soats){
                context.SOATs.Add(s);
            }
            context.SaveChanges();
        }
    }
}
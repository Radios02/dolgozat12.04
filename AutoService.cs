using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozat12._04
{
    internal class AutoService
    {
        private readonly AutoServiceDbContext db;

        public AutoService(AutoServiceDbContext db)
        {
            this.db = db;
        }

        public async Task Seed()
        {
            var Brands = new List<Brand>
            {
                new Brand { Name = "Opel" },
                new Brand { Name = "Ford" },
                new Brand { Name = "Honda" }
            };

            db.Brands.AddRange(Brands);

            var parts = new List<Part>
            {
                new Part { Name = "Olaj", Brands = null, Price = 3000 },
                new Part { Name = "Lámpa", Brands = null, Price = 500 },
                new Part { Name = "Fékpedél", Brands = Brands[0], Price = 5000 },
                new Part { Name = "Légszűrő", Brands = Brands[0], Price = 2000 },
                new Part { Name = "Légszűrő", Brands = Brands[0], Price = 5000 },
                new Part { Name = "Olaj szűrő", Brands = Brands[1], Price = 1500 },
                new Part { Name = "Fék lámpa", Brands = Brands[1], Price = 1200 },
                new Part { Name = "Kuplung", Brands = Brands[1], Price = 12000 },
                new Part { Name = "Akumlátor", Brands = Brands[2], Price = 8000 },
                new Part { Name = "Fluxuskondenzátor", Brands = Brands[2], Price = 8000000 },
                new Part { Name = "Felni", Brands = Brands[2], Price = 10000 }
            };

            db.Parts.AddRange(parts);
            await db.SaveChangesAsync();
            Console.WriteLine("Mentve.");
        }

        public async Task AddServiceLog()
        {
            var serviceLog = new ServiceLog();
            Console.WriteLine("Add meg a márkát:");
            serviceLog.Brand.Name = Console.ReadLine();

            Console.WriteLine("Add meg a modelt:");
            serviceLog.CarModel = Console.ReadLine();

            Console.WriteLine("Add meg a rendszámot:");
            serviceLog.LicensePlate = Console.ReadLine();

            Console.WriteLine("Add meg a kilóméterórát:");
            serviceLog.Mileage = int.Parse(Console.ReadLine());

            serviceLog.ServiceDate = DateTime.Now;

            db.ServiceLogs.Add(serviceLog);
            await db.SaveChangesAsync();
            Console.WriteLine($"Az ID azonosítóval létrehozott szolgáltatási napló: {serviceLog.Id}");
        }

        public async Task AddPartToServiceLog()
        {
            Console.WriteLine("Add meg az Id-t:");
            var serviceLogId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter part ID:");
            var partId = int.Parse(Console.ReadLine());

            Console.WriteLine("Add meg az árat:");
            var quantity = decimal.Parse(Console.ReadLine());

            var part = db.Parts.Find(partId);
            if (part == null)
            {
                Console.WriteLine("Alkatrész nem található!");
                return;
            }

            var serviceDetail = new ServiceDetail
            {
                ServiceLogId = serviceLogId,
                PartId = partId,
                Quantity = quantity,
                UnitPrice = part.Price
            };

            db.ServiceDetails.Add(serviceDetail);
            await db.SaveChangesAsync();
            Console.WriteLine("Alkatrész hozzáada a log-hoz.");
        }

        public void RevenueByBrand()
        {
            var revenue = db.ServiceDetails
                .Include(sd => sd.Part)
                .GroupBy(sd => sd.Part.Brands.Name)
                .Select(g => new
                {
                    Brand = g.Key,
                    TotalRevenue = g.Sum(sd => sd.TotalCost)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToList();

            Console.WriteLine("Bevételek márkánként:");
            foreach (var entry in revenue)
            {
                Console.WriteLine($"{entry.Brand}: {entry.TotalRevenue:C}");
            }
        }

        public void TopSellingParts()
        {
            var topParts = db.ServiceDetails
                .Include(sd => sd.Part)
                .GroupBy(sd => sd.Part.Name)
                .Select(g => new
                {
                    PartName = g.Key,
                    TotalQuantity = g.Sum(sd => sd.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(3)
                .ToList();

            Console.WriteLine("Top 3 eladott alaktrész:");
            foreach (var part in topParts)
            {
                Console.WriteLine($"{part.PartName}: {part.TotalQuantity}");
            }
        }

        public void MostServicedBrand()
        {
            var mostServiced = db.ServiceLogs
                .GroupBy(sl => sl.Brand)
                .Select(g => new
                {
                    Brand = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(b => b.Count)
                .FirstOrDefault();

            if (mostServiced != null)
            {
                Console.WriteLine($"Legtöbbet szetvizelt márka: {mostServiced.Brand} -val/vel {mostServiced.Count} szevizelve.");
            }
        }
    }
}

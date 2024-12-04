using dolgozat12._04;
using var db = new AutoServiceDbContext();
var service = new AutoService(db);

while (true)
{
    Console.WriteLine("Válasszon egy opciót:");
    Console.WriteLine("1 - Seed data");
    Console.WriteLine("2 - Szerviznapló hozzáadás");
    Console.WriteLine("3 - Alkatrész hozzáadása a szerviznaplóhoz");
    Console.WriteLine("4 - Bevételek márkánként");
    Console.WriteLine("5 - legtöbbet eladott alaktrészek");
    Console.WriteLine("6 - legtöbbet szervizelt márka");
    Console.WriteLine("0 - kilépés");

    var input = Console.ReadLine();

    if (Enum.TryParse<Menu>(input, out var choice))
    {
        switch (choice)
        {
            case Menu.SeedData:
                await service.Seed();
                break;
            case Menu.AddServiceLog:
                await service.AddServiceLog();
                break;
            case Menu.AddPartToServiceLog:
                await service.AddPartToServiceLog();
                break;
            case Menu.RevenueByBrand:
                service.RevenueByBrand();
                break;
            case Menu.TopSellingParts:
                service.TopSellingParts();
                break;
            case Menu.MostServicedBrand:
                service.MostServicedBrand();
                break;
            case Menu.Exit:
                return;
        }
    }
    else
    {
        Console.WriteLine("Érvénytelen választás, kérjük, próbálja meg újra.");
    }
}


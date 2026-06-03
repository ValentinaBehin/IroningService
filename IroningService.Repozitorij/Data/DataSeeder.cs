using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using System.Linq;
using System.Collections.Generic;

namespace IroningService.Repozitorij.Data;

public static class DataSeeder
{
    public static void SeedUsluge(RepozitorijContext context)
    {
        if (context.Usluge.Any()) return;

        var usluge = new List<UslugaPeglanja>
        {
            new UslugaPeglanja { Naziv = "Peglanje iznad 7kg", Cijena = 3.50m, Jedinica = MjernaJedinica.Kilogram },
            new UslugaPeglanja { Naziv = "Jakna", Cijena = 3.60m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Majica kratka", Cijena = 1.15m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Majica duga", Cijena = 1.25m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Majica s kapuljačom", Cijena = 1.35m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Vesta", Cijena = 1.35m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Košulja kratka", Cijena = 1.60m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Košulja duga", Cijena = 2.00m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Košulja zahtjevna", Cijena = 2.15m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Haljina kratka", Cijena = 2.30m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Haljina duga", Cijena = 2.90m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Haljina zahtjevnija", Cijena = 3.30m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Kombinezoni", Cijena = 2.90m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Hlače kratke", Cijena = 1.40m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Hlače duge", Cijena = 1.70m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Hlače na crtu", Cijena = 2.40m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Trenirke donji dio", Cijena = 1.40m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Suknja kratka", Cijena = 1.35m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Suknja duga", Cijena = 1.55m, Jedinica = MjernaJedinica.Komad },
            new UslugaPeglanja { Naziv = "Suknja zahtjevnija", Cijena = 1.70m, Jedinica = MjernaJedinica.Komad }
        };

        context.Usluge.AddRange(usluge);
        context.SaveChanges();
    }
}
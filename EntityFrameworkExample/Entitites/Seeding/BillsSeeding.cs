using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Entitites.Seeding
{
    public static class BillsSeeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var factura1 = new Bill() { Id = 2, CreationDate = new DateTime(2022, 1, 24) };

            var detalle1 = new List<BillDetails>()
    {
        new BillDetails(){Id = 3, BillId = factura1.Id, Price = 350.99m},
        new BillDetails(){Id = 4, BillId = factura1.Id, Price = 10},
        new BillDetails(){Id = 5, BillId = factura1.Id, Price = 45.50m},
    };

            var factura2 = new Bill() { Id = 3, CreationDate = new DateTime(2022, 1, 24) };

            var detalle2 = new List<BillDetails>()
    {
        new BillDetails(){Id = 6, BillId = factura2.Id, Price = 17.99m},
        new BillDetails(){Id = 7, BillId = factura2.Id, Price = 14},
        new BillDetails(){Id = 8, BillId = factura2.Id, Price = 45},
        new BillDetails(){Id = 9, BillId = factura2.Id, Price = 100},
    };

            var factura3 = new Bill() { Id = 4, CreationDate = new DateTime(2022, 1, 24) };

            var detalle3 = new List<BillDetails>()
    {
        new BillDetails(){Id = 10, BillId = factura3.Id, Price = 371},
        new BillDetails(){Id = 11, BillId = factura3.Id, Price = 114.99m},
        new BillDetails(){Id = 12, BillId = factura3.Id, Price = 425},
        new BillDetails(){Id = 13, BillId = factura3.Id, Price = 1000},
        new BillDetails(){Id = 14, BillId = factura3.Id, Price = 5},
        new BillDetails(){Id = 15, BillId = factura3.Id, Price = 2.99m},
    };

            var factura4 = new Bill() { Id = 5, CreationDate = new DateTime(2022, 1, 24) };

            var detalle4 = new List<BillDetails>()
    {
        new BillDetails(){Id = 16, BillId = factura4.Id, Price = 50},
    };

            modelBuilder.Entity<Bill>().HasData(factura1, factura2, factura3, factura4);
            modelBuilder.Entity<BillDetails>().HasData(detalle1);
            modelBuilder.Entity<BillDetails>().HasData(detalle2);
            modelBuilder.Entity<BillDetails>().HasData(detalle3);
            modelBuilder.Entity<BillDetails>().HasData(detalle4);
        }
    }

}

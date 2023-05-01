using Microsoft.EntityFrameworkCore;
using TD.WareHouse.DemoApp.Core.Domain.Models.Employees;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodIssues;
using TD.WareHouse.DemoApp.Core.Domain.Models.GoodsReceipts;
using TD.WareHouse.DemoApp.Core.Domain.Models.Items;
using TD.WareHouse.DemoApp.Core.Domain.Models.Locations;
using TD.WareHouse.DemoApp.Core.Domain.Models.Warehouses;
using TD.WareHouse.DemoApp.Core.Domain.Repositories;

namespace TD.WareHouse.DemoApp.Core.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLot> ItemLots { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<GoodsIssue> GoodsIssues { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasKey(i => i.ItemId);

            modelBuilder.Entity<ItemLot>()
                .HasKey(i => i.LotId);

            modelBuilder.Entity<Warehouse>()
                .HasKey(i => i.WarehouseId);

            modelBuilder.Entity<Location>()
                .HasKey(i => i.LocationId);

            modelBuilder.Entity<GoodsIssue>()
                .HasKey(i => i.GoodsIssueId);

            modelBuilder.Entity<GoodsReceipt>()
                .HasKey(i => i.GoodsReceiptId);
        }
    }
}

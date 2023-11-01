using Domain.Consts;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Database.Context
{
    public class EfContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Truck> Trucks { get; set; }

        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>(a =>
            {
                a.ToTable(nameof(Address), DbConsts.Schema);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.HasIndex(a => a.Id).IsUnique(true);
                //a.Property(a => a.сity).IsRequired();
                //a.HasOne(x => x.сity).WithOne().HasForeignKey<Address>(x => x.сity.Id);
                a.Property(a => a.address).IsRequired().HasColumnType("nvarchar(100)");
                //a.Property(a => a.Street).IsRequired();
                //a.Property(a => a.Build).IsRequired();
                //a.Property(a => a.Apartment).IsRequired(false);
            });

            builder.Entity<City>(c =>
            {
                c.ToTable(nameof(City), DbConsts.Schema);
                c.HasKey(c => c.Id);
                c.Property(x => x.Id).ValueGeneratedOnAdd();
                c.HasIndex(c => c.Id).IsUnique(true);
                c.Property(c => c.Name).IsRequired().HasColumnType("nvarchar(100)");
            });

            builder.Entity<Order>(o =>
            {
                o.ToTable(nameof(Order), DbConsts.Schema);
                o.HasKey(x => x.Id);
                o.Property(x => x.Id).ValueGeneratedOnAdd();
                o.HasIndex(a => a.Id).IsUnique(true);
                //o.Property(o => o.AddressSender).IsRequired();
                //o.Property(o => o.AddressRecipient).IsRequired();
                //o.Property(o => o.AddressSender).IsRequired();
                //o.Property(o => o.AddressRecipient).IsRequired();
                o.HasOne(o => o.AddressSender).WithMany().OnDelete(DeleteBehavior.Restrict);
                o.HasOne(o => o.AddressRecipient).WithMany().OnDelete(DeleteBehavior.Restrict);
                o.HasOne(o => o.Truck).WithMany().HasForeignKey(x => x.TruckId);
                o.Property(o => o.Weight).IsRequired().HasColumnType("float(15,2)");
                o.Property(o => o.PickupDt).IsRequired().HasColumnType("date");
                o.Property(o => o.CreatedDt).IsRequired();
                o.Property(o => o.OrderNumber).IsRequired().HasColumnType("nvarchar(100)"); ;
                o.Property(o => o.Price).IsRequired();
            });

            builder.Entity<PriceList>(o =>
            {
                o.ToTable(nameof(PriceList), DbConsts.Schema);
                o.HasKey(x => x.Id);
                o.Property(x => x.Id).ValueGeneratedOnAdd();
                o.HasIndex(a => a.Id).IsUnique(true);
                o.HasOne(o => o.CityStart).WithMany().OnDelete(DeleteBehavior.Restrict);
                o.HasOne(o => o.CityFinish).WithMany().OnDelete(DeleteBehavior.Restrict);
                //o.Property(o => o.CityStart).IsRequired();
                //o.Property(o => o.CityFinish).IsRequired();
                o.Property(o => o.Price).IsRequired();
            });

            builder.Entity<Truck>(o =>
            {
                o.ToTable(nameof(Truck), DbConsts.Schema);
                o.HasKey(x => x.Id);
                o.Property(x => x.Id).ValueGeneratedOnAdd();
                o.HasIndex(a => a.Id).IsUnique(true);
                o.Property(o => o.Weight).IsRequired().HasColumnType("float(15,2)");
                o.Property(o => o.Name).IsRequired().HasColumnType("nvarchar(100)"); ;
            });
        }
    }
}

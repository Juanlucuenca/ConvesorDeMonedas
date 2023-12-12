using ConvesorDeMonedas.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvesorDeMonedas.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> currencies { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Conversion> Conversions { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .HasOne(s => s.Subscription)
                 .WithMany(u => u.Users)
                 .HasForeignKey(u => u.SubscriptionId);

            modelBuilder.Entity<Conversion>()
                .HasOne(c => c.User)
                .WithMany(u => u.Conversions)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Conversion>()
                .HasOne(c => c.ToCurrency)
                .WithMany()
                .HasForeignKey(c => c.ToCurrencyId);

            modelBuilder.Entity<Conversion>()
                .HasOne(c => c.FromCurrency)
                .WithMany()
                .HasForeignKey(c => c.FromCurrencyId);


            modelBuilder.Entity<Subscription>().HasData(
                new Subscription() { Id = 1, Name = "Free", AmountOfConvertions = 3, Price = 0},
                new Subscription() { Id = 2, Name = "Trial", AmountOfConvertions = 8, Price = 5 },
                new Subscription() { Id = 3, Name = "Pro", AmountOfConvertions = 15, Price = 10 }
            );

              modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "Juan", Password = "pepe123", Role = Role.User, Mail = "juan@gmail.com", ConvertionsCount = 0, SubscriptionId = 1 },
                new User() { Id = 7, UserName = "Pedro", Password = "pelito123567", Role = Role.User, Mail = "pedro@gmail.com", ConvertionsCount = 0, SubscriptionId = 2 },
                new User() { Id = 9, UserName = "Marta", Password = "martalamejor", Role = Role.User, Mail = "marta@gmail.com", ConvertionsCount = 0, SubscriptionId = 3 }
            );

            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Name = "ARS", Symbol = "$", Ic = 0.002 },
                new Currency() { Id = 2, Name = "EUR", Symbol = "€", Ic = 1.09 },
                new Currency() { Id = 3, Name = "BRL", Symbol = "R$", Ic = 0.197 }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}

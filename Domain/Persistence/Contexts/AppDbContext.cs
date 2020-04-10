

using System;
using Microsoft.EntityFrameworkCore;
using WhoIsMyGDaddy.API.Domain.Models;

namespace WhoIsMyGDaddy.API.Domain.Persistence.Contexts
{

    public class AppDbContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<Person>().HasKey(p => p.Id);

            // TODO: modify , add othe props
            builder.Entity<Person>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Person>().Property(p => p.Surname);
            builder.Entity<Person>().Property(p => p.MotherId);
            builder.Entity<Person>().Property(p => p.FatherId);
            builder.Entity<Person>().Property(p => p.IdentityNumber);
            builder.Entity<Person>().Property(p => p.BirthDate);

            builder.Entity<Person>().HasData
            (
                new Person { Id = 1001, Name = "Tshepiso", Surname = "Mogapi", BirthDate = new DateTime(1950, 1, 18), IdentityNumber = "5001185555081" },
                new Person { Id = 1002, Name = "Samy", Surname = "Mitchell", BirthDate = new DateTime(1960, 2, 18), IdentityNumber = "6002185555081" },
                new Person { Id = 1003, Name = "Angel", Surname = "Thebe", FatherId = 1002, MotherId = 1001, BirthDate = new DateTime(1980, 3, 18), IdentityNumber = "8003185555081" },
                new Person { Id = 1004, Name = "Mariana", Surname = "Murilo", MotherId = 1001, BirthDate = new DateTime(1984, 4, 18), IdentityNumber = "8404185555081" },
                new Person { Id = 1005, Name = "Correia", Surname = "Melo", FatherId = 1003, BirthDate = new DateTime(1985, 8, 18), IdentityNumber = "8508185555081" },
                new Person { Id = 1006, Name = "Rivera", Surname = "Gutierrez", MotherId = 1004, BirthDate = new DateTime(1996, 8, 18), IdentityNumber = "9608185555081" },
                new Person { Id = 1007, Name = "Rodas", Surname = "Quintero", FatherId = 1005, BirthDate = new DateTime(1999, 8, 18), IdentityNumber = "9908185555081" },
                new Person { Id = 1008, Name = "Rhys", Surname = "Lilly", FatherId = 1006, BirthDate = new DateTime(2014, 8, 18), IdentityNumber = "1408185555081" },
                new Person { Id = 1009, Name = "Freddie", Surname = "Thomson", FatherId = 1006, BirthDate = new DateTime(2000, 8, 18), IdentityNumber = "0008185555081" },
                new Person { Id = 1010, Name = "Celina", Surname = "Liam", MotherId = 1004, BirthDate = new DateTime(2001, 8, 18), IdentityNumber = "0108185555081" }
            );
        }

    } 
}
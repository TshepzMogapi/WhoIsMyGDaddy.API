

using Microsoft.EntityFrameworkCore;
using WhoIsMyGDaddy.API.Domain.Models;

namespace WhoIsMyGDaddy.API.Domain.Persistence.Contexts {

    public class AppDbContext: DbContext {

        public DbSet<Person> Persons {get;set;}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Person>().ToTable("Persons");
            builder.Entity<Person>().HasKey(p => p.Id);

            // TODO: modify , add othe props
            builder.Entity<Person>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            builder.Entity<Person>().HasData
            (
                new Person { Id = 100, Name = "Tshepiso Mogapi" }, // Id set manually due to in-memory provider
                new Person { Id = 101, Name = "Sam Mitchell" }
            );
        }

    }
}
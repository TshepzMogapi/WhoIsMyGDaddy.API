

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhoIsMyGDaddy.API.Domain.Models;
using WhoIsMyGDaddy.API.Domain.Persistence.Contexts;
using WhoIsMyGDaddy.API.Domain.Repositories;
using static WhoIsMyGDaddy.API.Domain.Models.Person;

namespace WhoIsMyGDaddy.API.Persistence.Repositories
{


    public class PersonRepository : BaseRepository, IPersonRepository
    {

        public PersonRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Person>> GetAllListAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        class PersonCte : Person
        {

        }


        public IEnumerable<Person> GetData(Expression<Func<Person, bool>> predicate)
        {

            var result = Enumerable.Empty<Person>();

            var dbSet = _context.Set<Person>();


            var person = dbSet
            .Where(predicate).FirstOrDefault();

            StringBuilder varname1 = new StringBuilder();
            varname1.Append("WITH FamilyTree( Name, Surname, Id, FatherId, MotherId, BirthDate, IdentityNumber) \n");
            varname1.Append("AS ( \n");

            varname1.Append("SELECT Name, Surname, Id, FatherId, MotherId, BirthDate, IdentityNumber \n");
            varname1.Append("FROM Persons \n");
            varname1.Append("WHERE Id = {0} \n");

            varname1.Append("UNION ALL \n");
            varname1.Append("SELECT Node.Name, Node.Surname, Node.Id, Node.FatherId, Node.MotherId, Node.BirthDate, Node.IdentityNumber \n");
            varname1.Append("FROM Persons Node \n");
            varname1.Append("JOIN FamilyTree ft \n");
            varname1.Append("ON Node.MotherId = ft.Id \n");
            varname1.Append(" \n");
            varname1.Append("OR Node.FatherId = ft.Id \n");
            varname1.Append(") \n");
            varname1.Append("SELECT Name, Surname, Id, FatherId, MotherId, BirthDate, IdentityNumber \n");
            varname1.Append("FROM FamilyTree;");

            var descendants = dbSet
            .FromSqlRaw(varname1.ToString(), person.Id).ToList();
            
            return descendants;
        }

        public Task<IEnumerable<Person>> GetAllListAsync(Expression<Func<Person, Boolean>> predicate)
        {
            return Task.Run(() =>
            {
                return GetData(predicate);
            });
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


// GetAncestorByIdAsync
// GetDescendantsAsync

        public Task<IEnumerable<Person>> GetDescendantsAsync(Expression<Func<Person, Boolean>> predicate) {
            return Task.Run(() =>
            {
                return GetDescendants(predicate);
            });
        }
        public Task<IEnumerable<Person>> GetAncestorByIdAsync(Expression<Func<Person, Boolean>> predicate)
        {

            return Task.Run(() =>
            {
                return GetAncestors(predicate);
            });

        }

        public IEnumerable<Person> GetAncestors(Expression<Func<Person, bool>> predicate)
        {
            try
            {

                var dbSet = _context.Set<Person>();

                var person = dbSet
                .Where(predicate).First();

                if (person == null)
                {
                    return null;
                }

                StringBuilder varname1 = new StringBuilder();
                varname1.Append("WITH Ancestors (FatherId, MotherId, Id, Name, Surname, BirthDate,IdentityNumber) \n");
                varname1.Append("AS \n");
                varname1.Append("( \n");
                
                varname1.Append("    SELECT e.FatherId, e.MotherId, e.Id, e.Name, e.Surname, e.BirthDate, e.IdentityNumber \n");
                varname1.Append("         \n");
                varname1.Append("    FROM Persons AS e \n");
                varname1.Append("    WHERE e.id = {0} \n");
                varname1.Append("    UNION ALL \n");

                varname1.Append("    SELECT e.FatherId, e.MotherId, e.Id, e.Name, e.Surname, e.BirthDate, e.IdentityNumber \n");
                varname1.Append("         \n");
                varname1.Append("    FROM Persons AS e \n");
                varname1.Append("    INNER JOIN Ancestors AS d \n");
                varname1.Append("        ON e.Id = d.FatherId or e.Id = d.MotherId \n");
                varname1.Append(") \n");
                
                varname1.Append("SELECT FatherId, MotherId, Id, Name, Surname, BirthDate,IdentityNumber \n");
                varname1.Append("FROM Ancestors");


                var ancestor = dbSet
                .FromSqlRaw(varname1.ToString(), person.Id).ToList();


                return ancestor;


            }
            catch (Exception ex)
            {
                throw new Exception("Something bad happened : " + ex.Message);
            }

        }
        public IEnumerable<Person> GetDescendants(Expression<Func<Person, bool>> predicate)
        {
            try
            {

                var dbSet = _context.Set<Person>();

                var person = dbSet
                .Where(predicate).First();

                if (person == null)
                {
                    return new List<Person>();
                }

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

                if (descendants.Count == 0)
                {
                    return new List<Person>();
                }
                return descendants;


            }
            catch (Exception ex)
            {
                throw new Exception("Something bad happened : " + ex.Message);
            }


        }


        public async Task AddPersonsAsync(List<Person> personList) {
            await _context.Persons.AddRangeAsync(personList);
        }
      
    }
}
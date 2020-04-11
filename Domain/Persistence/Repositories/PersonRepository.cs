

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


        public Task<IEnumerable<Person>>  Get(Expression<Func<Person, Boolean>> predicate)
        {

            return Task.Run(() =>
            {
                return GetData(predicate);
            });

        }

        public IEnumerable<Person> GetData(Expression<Func<Person, bool>> predicate)
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
                varname1.Append("DECLARE @CodeId INT = 1005 \n");
                varname1.Append("  \n");
                varname1.Append(";");


                StringBuilder varname11 = new StringBuilder();
                varname11.Append("WITH cteID \n");
                varname11.Append("As \n");
                varname11.Append("( \n");
                varname11.Append("  \n");
                varname11.Append("    SELECT \n");
                varname11.Append("        Id, FatherId, MotherId, Name, BirthDate, Surname, IdentityNumber,1 AS CodePosition \n");
                varname11.Append("    FROM \n");
                varname11.Append("        Persons WHERE Id = @CodeId \n");
                varname11.Append("    UNION All \n");
                varname11.Append("    SELECT \n");
                varname11.Append("        ic.Id, ic.FatherId, ic.MotherId, ic.Name, ic.BirthDate, ic.Surname, ic.IdentityNumber,CodePosition + 1 \n");
                varname11.Append("    FROM Persons ic \n");
                varname11.Append("    INNER JOIN cteID cte ON ic.Id = cte.MotherId OR cte.FatherId = ic.Id \n");
                varname11.Append(") \n");
                varname11.Append("SELECT TOP 1 * FROM cteID \n");
                varname11.Append("ORDER BY CodePosition DESC");


                var ancestor = dbSet
                .FromSqlRaw(varname1.ToString(), person.Id).ToList();

                return ancestor;


            }
            catch (Exception ex)
            {
                throw new Exception("Something bad happened : " + ex.Message);
            }

        }
        public IEnumerable<Person> GetAllData(Expression<Func<Person, bool>> predicate)
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

        public Task<IEnumerable<Person>> GetAllListAsync(Expression<Func<Person, Boolean>> predicate)
        {
            return Task.Run(() =>
            {
                return GetAllData(predicate);
            });
        }
    }
}
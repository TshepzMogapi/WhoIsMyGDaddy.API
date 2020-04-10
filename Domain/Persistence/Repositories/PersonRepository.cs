

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhoIsMyGDaddy.API.Domain.Models;
using WhoIsMyGDaddy.API.Domain.Persistence.Contexts;
using WhoIsMyGDaddy.API.Domain.Repositories;

namespace WhoIsMyGDaddy.API.Persistence.Repositories {


    public class PersonRepository : BaseRepository, IPersonRepository {

        public PersonRepository(AppDbContext context): base(context) {
            
        }

        public async Task<IEnumerable<Person>> GetAllListAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public IEnumerable<Person> GetData(Expression<Func<Person, bool>> predicate) {

            var result = Enumerable.Empty<Person>();

            var dbSet =  _context.Set<Person>();

            result = dbSet.Where(predicate).ToList();

            return result;
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
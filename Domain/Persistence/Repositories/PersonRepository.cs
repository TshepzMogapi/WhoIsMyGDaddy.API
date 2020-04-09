

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhoIsMyGDaddy.API.Domain.Models;
using WhoIsMyGDaddy.API.Domain.Persistence.Contexts;
using WhoIsMyGDaddy.API.Domain.Repositories;

namespace WhoIsMyGDaddy.API.Persistence.Repositories {


    public class PersonRepository : BaseRepository, IPersonRepository {

        public PersonRepository(AppDbContext context): base(context) {
            
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            return await _context.Persons.ToListAsync();
        }
    }
}
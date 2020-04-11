

using System.Collections.Generic;
using System.Threading.Tasks;
using WhoIsMyGDaddy.API.Domain.Models;
using WhoIsMyGDaddy.API.Domain.Repositories;
using WhoIsMyGDaddy.API.Domain.Services;

namespace WhoIsMyGDaddy.API.Services {

    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository) {
            _personRepository = personRepository;
        }
        
        public async Task<IEnumerable<Person>> ListAsync()
        {
            return await _personRepository.GetAllListAsync();
        }

         public async Task<IEnumerable<Person>> Get(string identityNumber){
            return await _personRepository.Get(p => p.IdentityNumber == identityNumber);
         }

        public async Task<IEnumerable<Person>> GetAllListAsync(string identityNumber)
        {
            return await _personRepository.GetAllListAsync(p => p.IdentityNumber == identityNumber);
        }
    }
}


using System.Collections.Generic;
using System.Threading.Tasks;
using WhoIsMyGDaddy.API.Domain.Models;

namespace WhoIsMyGDaddy.API.Domain.Repositories {

    public interface IPersonRepository {
        Task<IEnumerable<Person>> ListAsync();
    }

}
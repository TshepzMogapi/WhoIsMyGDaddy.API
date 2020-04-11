using System.Collections.Generic;
using System.Threading.Tasks;
using WhoIsMyGDaddy.API.Domain.Models;

namespace WhoIsMyGDaddy.API.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> ListAsync(); 
        Task<IEnumerable<Person>> GetAllListAsync(string id);
        Task<IEnumerable<Person>> Get(string id);
    }   
}
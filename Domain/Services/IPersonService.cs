using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhoIsMyGDaddy.API.Domain.Models;

namespace WhoIsMyGDaddy.API.Domain.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> ListAsync(); 
        Task<IEnumerable<Person>> GetDescendantsAsync(string id);
        Task<IEnumerable<Person>> GetAncestorByIdAsync(string id);
    }   
}
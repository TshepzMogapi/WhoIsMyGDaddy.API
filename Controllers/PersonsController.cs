using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhoIsMyGDaddy.API.Domain.Models;
using WhoIsMyGDaddy.API.Domain.Services;

namespace WhoIsMyGDaddy.API.Domain.Controllers
{

    [Route("/api/[controller]")]
    public class PersonsController : Controller {

        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService) {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAllAsync(){

            var persons = await _personService.ListAsync();

            return persons;

        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Person>> GetAllByIdAsync(string id){

            return await _personService.GetAllListAsync(
                id);

        }


    }

}

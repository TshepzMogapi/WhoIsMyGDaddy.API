using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhoIsMyGDaddy.API.Domain.Models;
using WhoIsMyGDaddy.API.Domain.Services;

namespace WhoIsMyGDaddy.API.Domain.Controllers
{

    [Route("/api/[controller]")]
    public class AncestorsController : Controller {

        private readonly IPersonService _personService;

        public AncestorsController(IPersonService personService) {
            _personService = personService;
        }


        [HttpGet("{id}")]
        public async Task<IEnumerable<Person>> GetAncestorByIdAsync(string id){

            return await _personService.GetAncestorByIdAsync(
                id);

        }



    }

}



using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WhoIsMyGDaddy.API.Domain.Models;

namespace WhoIsMyGDaddy.API.Domain.Repositories {

    public interface IPersonRepository {
        Task<IEnumerable<Person>> GetAllListAsync();

        Task<IEnumerable<Person>> GetAllListAsync(Expression<Func<Person, bool>> predicate);
    }

}
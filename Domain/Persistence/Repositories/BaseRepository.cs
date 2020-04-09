

using WhoIsMyGDaddy.API.Domain.Persistence.Contexts;

namespace WhoIsMyGDaddy.API.Persistence.Repositories {

    public abstract class BaseRepository {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) {
            _context = context;
        }

        
    }
}
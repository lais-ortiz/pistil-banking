using Microsoft.EntityFrameworkCore;

namespace Pistil.Banking.Data.ContextFactory
{
    public class PistilDbContext : DbContext
    {
        public PistilDbContext(DbContextOptions<PistilDbContext> options)
            : base(options) { }
    }
}

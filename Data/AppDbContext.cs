
using Microsoft.EntityFrameworkCore;

namespace Empresa.Data
{
    public class AppDbContext : Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext
    {
        public AppDbContext (DbContextOptions options) : base() { }
    }

}

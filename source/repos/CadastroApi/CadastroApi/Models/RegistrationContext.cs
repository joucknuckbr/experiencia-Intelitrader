using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Models
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
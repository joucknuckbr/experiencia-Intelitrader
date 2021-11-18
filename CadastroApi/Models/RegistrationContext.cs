using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CadastroApi.Models
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
    }
}
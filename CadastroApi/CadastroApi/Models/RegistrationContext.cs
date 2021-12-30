using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroApi.Models
{
    public interface IRegistrationContext
    {
        DbSet<User> Users { get; set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);

        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);

        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;

        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;

        EntityEntry<TEntity> Add<TEntity>( TEntity entity) where TEntity : class;
    }
    public class RegistrationContext : DbContext, IRegistrationContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


    }
}
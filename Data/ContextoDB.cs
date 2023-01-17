using DesafioWebApi.Data.Map;
using DesafioWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebApi.Data
{
    public class ContextoDB : DbContext
    {
        public ContextoDB(DbContextOptions<ContextoDB> options) : base(options)
        {

        }

        public DbSet<ClienteModel> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}

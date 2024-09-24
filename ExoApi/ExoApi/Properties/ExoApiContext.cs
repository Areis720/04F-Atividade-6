using ExoApi.Domains;
using Microsoft.EntityFrameworkCore;

namespace ExoApi.Properties
{
    public class ExoApiContext : DbContext
    {
        public ExoApiContext()
        {
        }
        public ExoApiContext(DbContextOptions<ExoApiContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = DESKTOP-VHPVNNJ\\SQLEXPRESS; initial catalog = ExoApi; Integrated Security = True; TrustServerCertificate=True");
            }
        }
        public DbSet<Projeto> Projetos {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
    }
}
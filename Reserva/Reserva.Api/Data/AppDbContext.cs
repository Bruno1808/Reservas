using Microsoft.EntityFrameworkCore;
using Reserva.Domain.Domain;

namespace Reserva.Api.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Laboratorios> Laboratorios { get; set; }

        public DbSet<Usuario> Usuarios  { get; set; }

        public DbSet<Reservas> Reservas { get; set; }

       

    }
}

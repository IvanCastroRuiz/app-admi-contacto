using Microsoft.EntityFrameworkCore;
using AppContactos.Models;

namespace AppContactos.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
        }

        public DbSet<Contacto>? Contacto {get; set;}
    }
}
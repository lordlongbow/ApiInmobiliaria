namespace api_prueba.Modelos;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Inmueble> Inmueble { get; set; }
    public DbSet<Propietario> Propietario { get; set; }
    public DbSet<Inquilino> Inquilino { get; set; }
    public DbSet<Contrato> Contrato { get; set; }
    public DbSet<Tipo> Tipo { get; set; }
    public DbSet<Uso> Uso {get; set;}
    public DbSet<Pago> Pago { get; set; }
}

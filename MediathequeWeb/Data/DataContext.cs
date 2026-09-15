using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    private const string DbPath = "data.sqlite";

    public DbSet<Adherents> Adherents { get; set; }
    public DbSet<Auteurs> Auteurs { get; set; }
    public DbSet<Emprunts> Emprunts { get; set; }
    public DbSet<Exemplaires> Exemplaires { get; set; }
    public DbSet<Oeuvres> Oeuvres { get; set; }
    public DbSet<Reservations> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}

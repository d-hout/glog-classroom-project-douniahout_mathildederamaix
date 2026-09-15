using Microsoft.EntityFrameworkCore;

public class AdherentsRepository : IAdherentsRepository
{
    private readonly DataContext _db;

    public AdherentsRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Adherents> GetAllAdherents()
    {
        return _db
            .Adherents.Include(a => a.Emprunts)
                .ThenInclude(e => e.Exemplaire)
                    .ThenInclude(ex => ex!.Oeuvre)
            .ToList();
    }

    public Adherents? GetAdherent(int id)
    {
        return _db
            .Adherents.Include(a => a.Emprunts)
                .ThenInclude(e => e.Exemplaire)
                    .ThenInclude(ex => ex!.Oeuvre)
            .Include(a => a.Reservations)
            .FirstOrDefault(a => a.Id == id);
    }

    public void AddAdherent(Adherents adherent)
    {
        _db.Adherents.Add(adherent);
    }

    public void RemoveAdherent(Adherents adherent)
    {
        _db.Adherents.Remove(adherent);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}

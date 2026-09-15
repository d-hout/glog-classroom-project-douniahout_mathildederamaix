using Microsoft.EntityFrameworkCore;

public class EmpruntsRepository : IEmpruntsRepository
{
    private readonly DataContext _db;

    public EmpruntsRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Emprunts> GetAllEmprunts()
    {
        return _db
            .Emprunts.Include(a => a.Adherent)
            .Include(e => e.Exemplaire)
                .ThenInclude(o => o!.Oeuvre)
            .ToList();
    }

    public Emprunts? GetEmprunt(int id)
    {
        return _db
            .Emprunts.Include(a => a.Adherent)
            .Include(e => e.Exemplaire)
                .ThenInclude(o => o!.Oeuvre)
            .FirstOrDefault(p => p.Id == id);
    }

    public void AddEmprunt(Emprunts emprunt)
    {
        _db.Emprunts.Add(emprunt);
    }

    public void RemoveEmprunt(Emprunts emprunt)
    {
        _db.Emprunts.Remove(emprunt);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}

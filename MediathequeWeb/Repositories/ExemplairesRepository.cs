using Microsoft.EntityFrameworkCore;

public class ExemplairesRepository : IExemplairesRepository
{
    private readonly DataContext _db;

    public ExemplairesRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Exemplaires> GetExemplairesByOeuvre(int oeuvreId)
    {
        return _db
            .Exemplaires
            .Where(e => e.OeuvreId == oeuvreId)
            .Include(e => e.Oeuvre)
            .ToList();
    }

    public Exemplaires? GetExemplaire(int id)
    {
        return _db.Exemplaires.Include(o => o.Oeuvre).FirstOrDefault(e => e.Id == id);
    }

    public void AddExemplaire(Exemplaires exemplaire)
    {
        _db.Exemplaires.Add(exemplaire);
    }

    public void RemoveExemplaire(Exemplaires exemplaire)
    {
        _db.Exemplaires.Remove(exemplaire);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}

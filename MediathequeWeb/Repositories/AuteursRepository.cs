using Microsoft.EntityFrameworkCore;

public class AuteursRepository : IAuteursRepository
{
    private readonly DataContext _db;

    public AuteursRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Auteurs> GetAllAuteurs()
    {
        return _db.Auteurs.Include(o => o.Oeuvres).ToList();
    }

    public Auteurs? GetAuteur(int id)
    {
        return _db.Auteurs.Include(o => o.Oeuvres).FirstOrDefault(a => a.Id == id);
    }

    public void AddAuteur(Auteurs auteur)
    {
        _db.Auteurs.Add(auteur);
    }

    public void RemoveAuteur(Auteurs auteur)
    {
        _db.Auteurs.Remove(auteur);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}

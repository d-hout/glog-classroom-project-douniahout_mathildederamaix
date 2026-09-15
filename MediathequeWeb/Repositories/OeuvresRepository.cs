using Microsoft.EntityFrameworkCore;

public class OeuvresRepository : IOeuvresRepository
{
    private readonly DataContext _db;

    public OeuvresRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Oeuvres> GetAllOeuvres()
    {
        return _db.Oeuvres.Include(a => a.Auteurs).ToList();
    }

    public Oeuvres? GetOeuvre(int id)
    {
        return _db.Oeuvres.Include(a => a.Auteurs).FirstOrDefault(a => a.Id == id);
    }

    public void AddOeuvre(Oeuvres oeuvre)
    {
        _db.Oeuvres.Add(oeuvre);
    }

    public void RemoveOeuvre(Oeuvres oeuvre)
    {
        _db.Oeuvres.Remove(oeuvre);
    }

    public Auteurs? GetAuteur(int auteurId)
    {
        return _db.Auteurs.FirstOrDefault(a => a.Id == auteurId);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}

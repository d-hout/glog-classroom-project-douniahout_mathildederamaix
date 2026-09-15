using System.Collections.Generic;
using System.Linq;

public class MockOeuvresRepository : IOeuvresRepository
{
    private readonly List<Oeuvres> _oeuvres;
    private readonly List<Auteurs> _auteurs;
    private int _nextOeuvreId = 1;

    public MockOeuvresRepository()
    {
        _auteurs = new List<Auteurs>
    {
        new Auteurs
        {
            Id = 1,
            Nom = "Hugo",
            Prenom = "Victor",
        },
        new Auteurs
        {
            Id = 2,
            Nom = "Camus",
            Prenom = "Albert",
        },
    };

        _oeuvres = new List<Oeuvres>
    {
        new Oeuvres
        {
            Id = _nextOeuvreId++,
            Titre = "Livre A",
            Type = OeuvreType.Livre,
            Auteurs = _auteurs.First(a => a.Id == 1),
        },
        new Oeuvres
        {
            Id = _nextOeuvreId++,
            Titre = "Livre B",
            Type = OeuvreType.Livre,
            Auteurs = _auteurs.First(a => a.Id == 2),
        },
        new Oeuvres
        {
            Id = _nextOeuvreId++,
            Titre = "Candide",
            Type = OeuvreType.Livre,
            Auteurs = _auteurs.First(a => a.Id == 1),
        },
    };
    }


    public IEnumerable<Oeuvres> GetAllOeuvres() => _oeuvres;

    public Oeuvres? GetOeuvre(int id) => _oeuvres.FirstOrDefault(o => o.Id == id);

    public Auteurs? GetAuteur(int auteurId) => _auteurs.FirstOrDefault(a => a.Id == auteurId);

    public void AddOeuvre(Oeuvres oeuvre)
    {
        oeuvre.Id = _nextOeuvreId++;
        _oeuvres.Add(oeuvre);
    }

    public void RemoveOeuvre(Oeuvres oeuvre)
    {
        _oeuvres.Remove(oeuvre);
    }

    public void SaveChanges() { }
}

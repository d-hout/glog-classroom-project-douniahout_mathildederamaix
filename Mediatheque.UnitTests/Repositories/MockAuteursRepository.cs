using System.Collections.Generic;
using System.Linq;

public class MockAuteursRepository : IAuteursRepository
{
    private readonly List<Auteurs> _auteurs = new();

    public MockAuteursRepository()
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
    }

    public IEnumerable<Auteurs> GetAllAuteurs() => _auteurs;

    public Auteurs? GetAuteur(int id) => _auteurs.FirstOrDefault(a => a.Id == id);

    public void AddAuteur(Auteurs auteur)
    {
        auteur.Id = _auteurs.Count > 0 ? _auteurs.Max(a => a.Id) + 1 : 1;
        _auteurs.Add(auteur);
    }

    public void RemoveAuteur(Auteurs auteur) => _auteurs.Remove(auteur);

    public void SaveChanges() { }
}

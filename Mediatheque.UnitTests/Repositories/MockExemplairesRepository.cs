using System.Collections.Generic;
using System.Linq;

public class MockExemplairesRepository : IExemplairesRepository
{
    private readonly List<Exemplaires> _exemplaires;
    private int _nextId = 1;

    public MockExemplairesRepository()
    {
        _exemplaires = new List<Exemplaires>
        {
            new Exemplaires
            {
                Id = _nextId++,
                OeuvreId = 1,
                Oeuvre = new Oeuvres
                {
                    Id = 1,
                    Titre = "Livre A",
                    AuteurId = 1,
                },
                Statut = StatutExemplaire.Disponible,
            },
            new Exemplaires
            {
                Id = _nextId++,
                OeuvreId = 1,
                Oeuvre = new Oeuvres
                {
                    Id = 1,
                    Titre = "Livre A",
                    AuteurId = 1,
                },
                Statut = StatutExemplaire.Emprunte,
            },
            new Exemplaires
            {
                Id = _nextId++,
                OeuvreId = 2,
                Oeuvre = new Oeuvres
                {
                    Id = 2,
                    Titre = "Livre B",
                    AuteurId = 2,
                },
                Statut = StatutExemplaire.Disponible,
            },
            new Exemplaires
            {
                Id = _nextId++,
                OeuvreId = 2,
                Oeuvre = new Oeuvres
                {
                    Id = 2,
                    Titre = "Livre B",
                    AuteurId = 2,
                },
                Statut = StatutExemplaire.Emprunte,
            },
            new Exemplaires
            {
                Id = _nextId++,
                OeuvreId = 3,
                Oeuvre = new Oeuvres
                {
                    Id = 3,
                    Titre = "Candide",
                    AuteurId = 1,
                },
                Statut = StatutExemplaire.Disponible,
            },
        };
    }

    public IEnumerable<Exemplaires> GetExemplairesByOeuvre(int oeuvreId)
    {
        return _exemplaires.Where(e => e.OeuvreId == oeuvreId);
    }

    public Exemplaires? GetExemplaire(int id)
    {
        return _exemplaires.FirstOrDefault(e => e.Id == id);
    }

    public void AddExemplaire(Exemplaires exemplaire)
    {
        exemplaire.Id = _nextId++;
        _exemplaires.Add(exemplaire);
    }

    public void RemoveExemplaire(Exemplaires exemplaire)
    {
        _exemplaires.Remove(exemplaire);
    }

    public void SaveChanges() { }
}

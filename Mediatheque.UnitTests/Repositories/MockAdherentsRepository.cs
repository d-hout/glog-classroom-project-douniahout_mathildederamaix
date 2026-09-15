using System.Collections.Generic;
using System.Linq;

public class MockAdherentsRepository : IAdherentsRepository
{
    private readonly List<Adherents> _adherents;

    public MockAdherentsRepository()
    {
        _adherents = new List<Adherents>
        {
            new Adherents
            {
                Id = 1,
                Nom = "Durand",
                Prenom = "Jean",
                Email = "jean.durand@email.com",
                DateInscription = DateOnly.FromDateTime(System.DateTime.Now),
            },
            new Adherents
            {
                Id = 2,
                Nom = "Martin",
                Prenom = "Claire",
                Email = "claire.martin@email.com",
                DateInscription = DateOnly.FromDateTime(System.DateTime.Now),
            },
        };
    }

    public IEnumerable<Adherents> GetAllAdherents() => _adherents;

    public Adherents? GetAdherent(int id) => _adherents.FirstOrDefault(a => a.Id == id);

    public void AddAdherent(Adherents adherent)
    {
        adherent.Id = _adherents.Max(a => a.Id) + 1;
        _adherents.Add(adherent);
    }

    public void RemoveAdherent(Adherents adherent) => _adherents.Remove(adherent);

    public void SaveChanges() { }
}

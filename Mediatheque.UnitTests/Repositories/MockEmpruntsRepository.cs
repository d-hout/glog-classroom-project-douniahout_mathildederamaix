using System.Collections.Generic;
using System.Linq;

public class MockEmpruntsRepository : IEmpruntsRepository
{
    private readonly List<Emprunts> _emprunts;
    private int _nextId = 1;

    public MockEmpruntsRepository()
    {
        _emprunts = new List<Emprunts>();
    }

    public IEnumerable<Emprunts> GetAllEmprunts() => _emprunts;

    public Emprunts? GetEmprunt(int id) => _emprunts.FirstOrDefault(e => e.Id == id);

    public void AddEmprunt(Emprunts emprunt)
    {
        emprunt.Id = _nextId++;
        _emprunts.Add(emprunt);
    }

    public void RemoveEmprunt(Emprunts emprunt) => _emprunts.Remove(emprunt);

    public void SaveChanges() { }
}

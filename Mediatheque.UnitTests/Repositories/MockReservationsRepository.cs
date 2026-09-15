using System.Collections.Generic;
using System.Linq;

public class MockReservationsRepository : IReservationsRepository
{
    private readonly List<Reservations> _reservations;
    private int _nextId = 1;

    public MockReservationsRepository()
    {
        _reservations = new List<Reservations>
        {
            new Reservations
            {
                Id = _nextId++,
                Adherent = new Adherents
                {
                    Id = 1,
                    Nom = "Dupont",
                    Prenom = "Jean",
                    Email = "jean.dupont@email.com",
                },
                Oeuvre = new Oeuvres { Id = 1, Titre = "Livre A" },
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
            new Reservations
            {
                Id = _nextId++,
                Adherent = new Adherents
                {
                    Id = 2,
                    Nom = "Durand",
                    Prenom = "Alice",
                    Email = "alice.durand@email.com",
                },
                Oeuvre = new Oeuvres { Id = 2, Titre = "Livre B" },
                DateReservation = DateOnly.FromDateTime(DateTime.Now),
                Statut = StatutReservation.Active,
            },
        };
    }

    public IEnumerable<Reservations> GetReservationsActive()
    {
        return _reservations.Where(r => r.Statut == StatutReservation.Active);
    }

    public IEnumerable<Reservations> GetReservationsByAdherent(int adherentId)
    {
        return _reservations.Where(r => r.Adherent?.Id == adherentId);
    }

    public IEnumerable<Reservations> GetReservationsByOeuvre(int oeuvreId)
    {
        return _reservations.Where(r => r.Oeuvre?.Id == oeuvreId);
    }

    public Reservations? GetReservation(int id)
    {
        return _reservations.FirstOrDefault(r => r.Id == id);
    }

    public void AddReservation(Reservations reservation)
    {
        reservation.Id = _nextId++;
        _reservations.Add(reservation);
    }

    public void RemoveReservation(Reservations reservation)
    {
        _reservations.Remove(reservation);
    }

    public void SaveChanges() { }
}

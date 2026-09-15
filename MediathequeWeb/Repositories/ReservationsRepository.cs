using Microsoft.EntityFrameworkCore;

public class ReservationsRepository : IReservationsRepository
{
    private readonly DataContext _db;

    public ReservationsRepository(DataContext db)
    {
        _db = db;
    }

    public IEnumerable<Reservations> GetReservationsActive()
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .Where(r => r.Statut == StatutReservation.Active)
            .ToList();
    }

    public IEnumerable<Reservations> GetReservationsByAdherent(int adherentId)
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .Where(r => r.Adherent!.Id == adherentId)
            .ToList();
    }

    public Reservations? GetReservation(int id)
    {
        return _db.Reservations.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Reservations> GetReservationsByOeuvre(int oeuvreId)
    {
        return _db
            .Reservations.Include(r => r.Adherent)
            .Include(r => r.Oeuvre)
            .Where(r => r.Oeuvre!.Id == oeuvreId)
            .ToList();
    }

    public void AddReservation(Reservations reservation)
    {
        _db.Reservations.Add(reservation);
    }

    public void RemoveReservation(Reservations reservation)
    {
        _db.Reservations.Remove(reservation);
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}

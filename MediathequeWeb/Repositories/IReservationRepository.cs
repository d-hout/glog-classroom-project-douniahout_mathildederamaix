public interface IReservationsRepository
{
    IEnumerable<Reservations> GetReservationsActive();
    IEnumerable<Reservations> GetReservationsByAdherent(int adherentId);
    IEnumerable<Reservations> GetReservationsByOeuvre(int oeuvreId);
    Reservations? GetReservation(int id);
    void AddReservation(Reservations reservation);
    void RemoveReservation(Reservations reservation);
    void SaveChanges();
}

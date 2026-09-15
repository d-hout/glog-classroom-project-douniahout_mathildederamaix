public class ReservationsService
{
    private readonly IReservationsRepository _reservationsRepo;
    private readonly IAdherentsRepository _adherentsRepo;
    private readonly IOeuvresRepository _oeuvresRepo;
    private readonly IExemplairesRepository _exemplairesRepo;

    public ReservationsService(
        IReservationsRepository reservationsRepo,
        IAdherentsRepository adherentsRepo,
        IOeuvresRepository oeuvresRepo,
        IExemplairesRepository exemplairesRepo
    )
    {
        _reservationsRepo = reservationsRepo;
        _adherentsRepo = adherentsRepo;
        _oeuvresRepo = oeuvresRepo;
        _exemplairesRepo = exemplairesRepo;
    }

    public IEnumerable<Reservations> GetAllReservationsActive()
    {
        var reservations = _reservationsRepo
            .GetReservationsActive()
            .Where(r => r.Statut == StatutReservation.Active);
        return reservations;
    }

    public IEnumerable<Reservations> GetReservationsByAdherent(int id)
    {
        var reservations = _reservationsRepo
            .GetReservationsByAdherent(id)
            .Where(r => r.Statut == StatutReservation.Active);
        return reservations;
    }

    public IEnumerable<Reservations> GetReservationsByOeuvre(int id)
    {
        var reservations = _reservationsRepo
            .GetReservationsByOeuvre(id)
            .Where(r => r.Statut == StatutReservation.Active);
        return reservations;
    }

    public Reservations CreateReservation(int adherentId, int oeuvreId)
    {
        var adherent = _adherentsRepo.GetAdherent(adherentId);
        if (adherent == null)
            throw new NotFoundException("Adhérent non trouvé.");

        var oeuvre = _oeuvresRepo.GetOeuvre(oeuvreId);
        if (oeuvre == null)
            throw new NotFoundException("Œuvre non trouvée.");

        bool exemplaireDisponible = _exemplairesRepo
            .GetExemplairesByOeuvre(oeuvre.Id)
            .Any(e => e.Statut == StatutExemplaire.Disponible);
        if (exemplaireDisponible)
            throw new DomainValidationException(
                "Un exemplaire est disponible pour cette œuvre."
            );

        var reservation = new Reservations
        {
            Adherent = adherent,
            Oeuvre = oeuvre,
            DateReservation = DateOnly.FromDateTime(DateTime.Now),
            Statut = StatutReservation.Active,
        };

        _reservationsRepo.AddReservation(reservation);
        _reservationsRepo.SaveChanges();

        return reservation;
    }

    public bool DeleteReservation(int id)
    {
        var reservation = _reservationsRepo.GetReservation(id);
        if (reservation == null)
            throw new NotFoundException("Réservation non trouvée.");

        reservation.Statut = StatutReservation.Annulee;
        _reservationsRepo.RemoveReservation(reservation);
        _reservationsRepo.SaveChanges();
        return true;
    }
}

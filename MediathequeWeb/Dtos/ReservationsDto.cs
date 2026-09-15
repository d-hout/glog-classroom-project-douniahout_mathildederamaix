public class ReservationsDto
{
    public int Id { get; set; }
    public int AdherentId { get; set; }
    public int OeuvreId { get; set; }
    public DateOnly DateReservation { get; set; }
    public string? Statut { get; set; }

    public static ReservationsDto FromModel(Reservations reservation)
    {
        var dto = new ReservationsDto
        {
            Id = reservation.Id,
            AdherentId = reservation.Adherent!.Id,
            OeuvreId = reservation.Oeuvre!.Id,
            DateReservation = reservation.DateReservation,
            Statut = reservation.Statut.ToString(),
        };
        return dto;
    }
}

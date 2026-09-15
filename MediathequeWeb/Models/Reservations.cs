public enum StatutReservation
{
    Active,
    Disponible,
    Terminee,
    Annulee,
}

public class Reservations
{
    public int Id { get; set; }
    public Oeuvres? Oeuvre { get; set; }
    public Adherents? Adherent { get; set; }
    public DateOnly DateReservation { get; set; }
    public StatutReservation Statut { get; set; }
}

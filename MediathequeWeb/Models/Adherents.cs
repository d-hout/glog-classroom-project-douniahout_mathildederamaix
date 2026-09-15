public class Adherents
{
    public int Id { get; set; }
    public string Nom { get; set; } = "";
    public string Prenom { get; set; } = "";
    public string Email { get; set; } = "";
    public DateOnly DateInscription { get; set; }
    public List<Emprunts> Emprunts { get; } = [];
    public List<Reservations> Reservations { get; } = [];
}

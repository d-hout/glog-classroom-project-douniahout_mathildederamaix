public enum StatutExemplaire
{
    Disponible,
    Emprunte,
}

public class Exemplaires
{
    public int Id { get; set; }
    public int OeuvreId { get; set; }
    public Oeuvres? Oeuvre { get; set; }
    public StatutExemplaire Statut { get; set; }
}

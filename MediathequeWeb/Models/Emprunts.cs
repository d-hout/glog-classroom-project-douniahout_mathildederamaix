public class Emprunts
{
    public int Id { get; set; }

    public Adherents? Adherent { get; set; }
    public Exemplaires? Exemplaire { get; set; }
    public DateOnly DateEmprunt { get; set; }
    public DateOnly DateRetourPrevue { get; set; }
    public DateOnly? DateRetourReelle { get; set; }
}

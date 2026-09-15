public class EmpruntsDto
{
    public int Id { get; set; }
    public int ExemplaireId { get; set; }
    public string? Oeuvre { get; set; }
    public string? Adherent { get; set; }
    public DateOnly DateEmprunt { get; set; }
    public DateOnly DateRetourPrevue { get; set; }
    public DateOnly? DateRetourReelle { get; set; }

    public static EmpruntsDto FromModel(Emprunts emprunt)
    {
        var dto = new EmpruntsDto
        {
            Id = emprunt.Id,
            ExemplaireId = emprunt.Exemplaire?.Id ?? 0, // si exemplaire est null on met 0
            Oeuvre = emprunt.Exemplaire?.Oeuvre?.Titre ?? "", // si le resultat est vide on renvoie une chaine vide
            Adherent = (emprunt.Adherent?.Prenom ?? "") + " " + (emprunt.Adherent?.Nom ?? ""),
            DateEmprunt = emprunt.DateEmprunt,
            DateRetourPrevue = emprunt.DateRetourPrevue,
            DateRetourReelle = emprunt.DateRetourReelle,
        };
        return dto;
    }
}

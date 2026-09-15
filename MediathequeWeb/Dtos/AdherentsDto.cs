public class AdherentsDto
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public string? Email { get; set; }
    public DateOnly DateInscription { get; set; }
    public List<string> Emprunts { get; set; } = [];

    public static AdherentsDto FromModel(Adherents adherent)
    {
        var dto = new AdherentsDto
        {
            Id = adherent.Id,
            Nom = adherent.Nom,
            Prenom = adherent.Prenom,
            Email = adherent.Email,
            DateInscription = adherent.DateInscription,
            Emprunts = adherent.Emprunts?.Select(e => e.Exemplaire!.Oeuvre!.Titre).ToList() ?? [],
            // revoir si exemplaires et oeuvres peuvent être null
        };
        return dto;
    }
}

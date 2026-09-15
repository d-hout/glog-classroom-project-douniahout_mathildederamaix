public class ExemplairesDto
{
    public int Id { get; set; }
    public string? OeuvreTitre { get; set; }
    public string? Statut { get; set; }

    public static ExemplairesDto FromModel(Exemplaires exemplaire)
    {
        var dto = new ExemplairesDto
        {
            Id = exemplaire.Id,
            OeuvreTitre = exemplaire.Oeuvre?.Titre,
            Statut = exemplaire.Statut.ToString(),
        };
        return dto;
    }
}

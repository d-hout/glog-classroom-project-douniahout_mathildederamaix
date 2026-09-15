public class AuteursDto
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public List<string> Oeuvres { get; set; } = [];

    public static AuteursDto FromModel(Auteurs auteur)
    {
        var dto = new AuteursDto
        {
            Id = auteur.Id,
            Nom = auteur.Nom,
            Prenom = auteur.Prenom,
            Oeuvres = auteur.Oeuvres?.Select(o => o.Titre).ToList() ?? [],
        };
        return dto;
    }
}

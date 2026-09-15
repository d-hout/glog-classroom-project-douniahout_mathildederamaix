public class OeuvresDto
{
    public int Id { get; set; }
    public string? Titre { get; set; }
    public string? Type { get; set; }
    public string? Auteur { get; set; }

    public static OeuvresDto FromModel(Oeuvres oeuvre)
    {
        var dto = new OeuvresDto
        {
            Id = oeuvre.Id,
            Titre = oeuvre.Titre,
            Type = oeuvre.Type.ToString(),
            Auteur = oeuvre.Auteurs?.Prenom + " " + oeuvre.Auteurs?.Nom,
        };
        return dto;
    }
}

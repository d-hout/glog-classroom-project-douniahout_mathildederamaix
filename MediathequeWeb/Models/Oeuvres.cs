public enum OeuvreType
{
    Livre,
    BD,
    DVD,
    CD,
}

public class Oeuvres
{
    public int Id { get; set; }
    public string Titre { get; set; } = "";
    public OeuvreType Type { get; set; }
    public int AuteurId { get; set; }
    public Auteurs? Auteurs { get; set; }
}

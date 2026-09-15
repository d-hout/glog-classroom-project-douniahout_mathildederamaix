public class Auteurs
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public List<Oeuvres> Oeuvres { get; set; } = [];
}

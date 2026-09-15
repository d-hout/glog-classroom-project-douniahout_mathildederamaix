public interface IOeuvresRepository
{
    IEnumerable<Oeuvres> GetAllOeuvres();
    Oeuvres? GetOeuvre(int id);
    Auteurs? GetAuteur(int auteurId);
    void AddOeuvre(Oeuvres oeuvre);
    void RemoveOeuvre(Oeuvres oeuvre);
    void SaveChanges();
}

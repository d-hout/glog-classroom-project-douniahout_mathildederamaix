public interface IAuteursRepository
{
    IEnumerable<Auteurs> GetAllAuteurs();
    Auteurs? GetAuteur(int id);
    void AddAuteur(Auteurs auteur);
    void RemoveAuteur(Auteurs auteur);
    void SaveChanges();
}

public interface IExemplairesRepository
{
    IEnumerable<Exemplaires> GetExemplairesByOeuvre(int oeuvreId);
    Exemplaires? GetExemplaire(int id);
    void AddExemplaire(Exemplaires exemplaire);
    void RemoveExemplaire(Exemplaires exemplaire);
    void SaveChanges();
}

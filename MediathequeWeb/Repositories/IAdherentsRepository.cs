public interface IAdherentsRepository
{
    IEnumerable<Adherents> GetAllAdherents();
    Adherents? GetAdherent(int id);
    void AddAdherent(Adherents adherent);
    void RemoveAdherent(Adherents adherent);
    void SaveChanges();
}

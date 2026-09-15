public interface IEmpruntsRepository
{
    IEnumerable<Emprunts> GetAllEmprunts();
    Emprunts? GetEmprunt(int id);
    void AddEmprunt(Emprunts exemplaire);
    void RemoveEmprunt(Emprunts exemplaire);
    void SaveChanges();
}

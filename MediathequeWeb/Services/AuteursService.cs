public class AuteursService
{
    private readonly IAuteursRepository _repo;

    public AuteursService(IAuteursRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Auteurs> GetAllAuteurs()
    {
        var auteurs = _repo.GetAllAuteurs();
        if (auteurs == null)
            throw new NotFoundException("Aucun auteur trouvé.");
        return auteurs;
    }

    public Auteurs? GetAuteurById(int id)
    {
        var auteur = _repo.GetAuteur(id);
        if (auteur == null)
            throw new NotFoundException("Auteur non trouvé.");
        return auteur;
    }

    public Auteurs CreateAuteur(string nom, string prenom)
    {
        var auteur = new Auteurs { Nom = nom, Prenom = prenom };

        _repo.AddAuteur(auteur);
        _repo.SaveChanges();
        return auteur;
    }

    public Auteurs? UpdateAuteur(int id, string nom, string prenom)
    {
        var auteur = _repo.GetAuteur(id);
        if (auteur == null)
            throw new NotFoundException("Auteur non trouvé");

        auteur.Nom = nom;
        auteur.Prenom = prenom;

        _repo.SaveChanges();
        return auteur;
    }

    public void DeleteAuteur(int id)
    {
        var auteur = _repo.GetAuteur(id);
        if (auteur == null)
            throw new NotFoundException("Auteur non trouvé");
        _repo.RemoveAuteur(auteur);
        _repo.SaveChanges();
    }
}

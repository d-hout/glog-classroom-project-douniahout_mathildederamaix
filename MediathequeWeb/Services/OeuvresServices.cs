public class OeuvresService
{
    private readonly IOeuvresRepository _repo;
    private readonly IExemplairesRepository _repoExemplaires;

    public OeuvresService(IOeuvresRepository repo, IExemplairesRepository repoExemplaires)
    {
        _repo = repo;
        _repoExemplaires = repoExemplaires;
    }

    public IEnumerable<Oeuvres> GetAllOeuvres()
    {
        var oeuvres = _repo.GetAllOeuvres();
        if (oeuvres == null)
            throw new NotFoundException("Oeuvre non trouvée.");
        return oeuvres;
    }

    public Oeuvres GetOeuvreById(int id)
    {
        var oeuvre = _repo.GetOeuvre(id);
        if (oeuvre == null)
            throw new NotFoundException("Oeuvre non trouvée.");
        return oeuvre;
    }

    public Oeuvres CreateOeuvre(string titre, OeuvreType type, int auteurId)
    {
        var auteur = _repo.GetAuteur(auteurId);
        if (auteur == null)
            throw new NotFoundException("Auteur non trouvé.");

        var oeuvre = new Oeuvres
        {
            Titre = titre,
            Type = type,
            Auteurs = auteur,
        };

        _repo.AddOeuvre(oeuvre);
        _repo.SaveChanges();
        return oeuvre;
    }

    public Oeuvres UpdateOeuvre(int id, string titre, OeuvreType type)
    {
        var oeuvre = _repo.GetOeuvre(id);
        if (oeuvre == null)
        {
            throw new NotFoundException("Oeuvre non trouvée.");
        }

        oeuvre.Titre = titre;
        oeuvre.Type = type;

        _repo.SaveChanges();
        return oeuvre;
    }

    public void DeleteOeuvre(int id)
    {
        var oeuvre = _repo.GetOeuvre(id);
        if (oeuvre == null)
            throw new NotFoundException("Oeuvre non trouvée.");

        _repo.RemoveOeuvre(oeuvre);
        _repo.SaveChanges();
    }
}

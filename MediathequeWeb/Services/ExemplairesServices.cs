public class ExemplairesService
{
    private readonly IExemplairesRepository _exemplairesRepo;
    private readonly IOeuvresRepository _oeuvresRepo;

    public ExemplairesService(
        IExemplairesRepository exemplairesRepo,
        IOeuvresRepository oeuvresRepo
    )
    {
        _exemplairesRepo = exemplairesRepo;
        _oeuvresRepo = oeuvresRepo;
    }

    public IEnumerable<Exemplaires> GetAllExemplairesByOeuvre(int oeuvreId)
    {
        var oeuvre = _oeuvresRepo.GetOeuvre(oeuvreId);
        if (oeuvre == null)
            throw new NotFoundException("Oeuvre non trouvée.");

        var exemplaires = _exemplairesRepo.GetExemplairesByOeuvre(oeuvreId);
        return exemplaires;
    }

    public Exemplaires GetExemplaireById(int id)
    {
        var exemplaire = _exemplairesRepo.GetExemplaire(id);
        if (exemplaire == null)
            throw new NotFoundException($"Exemplaire avec cet identifiant non trouvé");
        return exemplaire;
    }

    public Exemplaires CreateExemplaire(int id)
    {
        var oeuvre = _oeuvresRepo.GetOeuvre(id);
        if (oeuvre == null)
            throw new NotFoundException("Oeuvre non trouvée");

        var exemplaire = new Exemplaires { Oeuvre = oeuvre, Statut = StatutExemplaire.Disponible };

        _exemplairesRepo.AddExemplaire(exemplaire);
        _exemplairesRepo.SaveChanges();

        return exemplaire;
    }

    public Exemplaires UpdateExemplaire(int id, int? nouveauOeuvreId = null)
    {
        var exemplaire = _exemplairesRepo.GetExemplaire(id);
        if (exemplaire == null)
            throw new NotFoundException("Exemplaire non trouvé");

        if (nouveauOeuvreId != null)
        {
            var oeuvre = _oeuvresRepo.GetOeuvre(nouveauOeuvreId ?? 0);
            if (oeuvre == null)
                throw new NotFoundException("Oeuvre non trouvée");

            exemplaire.Oeuvre = oeuvre;
        }

        _exemplairesRepo.SaveChanges();
        return exemplaire;
    }

    public void DeleteExemplaire(int id)
    {
        var exemplaire = _exemplairesRepo.GetExemplaire(id);
        if (exemplaire == null)
            throw new NotFoundException("Exemplaire non trouvé");

        _exemplairesRepo.RemoveExemplaire(exemplaire!);
        _exemplairesRepo.SaveChanges();
    }
}

public class AdherentsService
{
    private readonly IAdherentsRepository _repo;

    public AdherentsService(IAdherentsRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<Adherents> GetAllAdherents()
    {
        var adherents = _repo.GetAllAdherents();
        if (adherents == null)
            throw new NotFoundException("Aucun adhérent trouvé.");
        return adherents;
    }

    public Adherents GetAdherentById(int id)
    {
        var adherent = _repo.GetAdherent(id);
        if (adherent == null)
            throw new NotFoundException("Adhérent non trouvé.");
        return adherent;
    }

    public Adherents CreateAdherent(string nom, string prenom, string email)
    {
        var adherent = new Adherents
        {
            Nom = nom,
            Prenom = prenom,
            Email = email,
            DateInscription = DateOnly.FromDateTime(DateTime.Now),
        };

        _repo.AddAdherent(adherent);
        _repo.SaveChanges();
        return adherent;
    }

    public Adherents UpdateAdherent(int id, string nom, string prenom, string email)
    {
        var adherent = GetAdherentById(id);
        if (adherent == null)
            throw new NotFoundException("Adhérent non trouvé.");
        adherent.Nom = nom;
        adherent.Prenom = prenom;
        adherent.Email = email;

        _repo.SaveChanges();
        return adherent;
    }

    public void DeleteAdherent(int id)
    {
        var adherent = GetAdherentById(id);
        if (adherent == null)
            throw new NotFoundException("Adhérent non trouvé.");
        if (adherent.Emprunts.Any() || adherent.Reservations.Any())
            throw new DomainValidationException(
                "Impossible de supprimer cet adhérent : il a des emprunts ou réservations en cours.");
        _repo.RemoveAdherent(adherent);
        _repo.SaveChanges();
    }
}

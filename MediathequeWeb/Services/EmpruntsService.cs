public class EmpruntsService
{
    private readonly IEmpruntsRepository _empruntsRepo;
    private readonly IAdherentsRepository _adherentsRepo;
    private readonly IExemplairesRepository _exemplairesRepo;
    private readonly IReservationsRepository _reservationsRepo;

    public EmpruntsService(
        IEmpruntsRepository empruntsRepo,
        IAdherentsRepository adherentsRepo,
        IExemplairesRepository exemplairesRepo,
        IReservationsRepository reservationsRepo
    )
    {
        _empruntsRepo = empruntsRepo;
        _adherentsRepo = adherentsRepo;
        _exemplairesRepo = exemplairesRepo;
        _reservationsRepo = reservationsRepo;
    }

    public IEnumerable<Emprunts> GetAllEmprunts()
    {
        var emprunts = _empruntsRepo.GetAllEmprunts();
        if (emprunts == null)
            throw new NotFoundException($"Aucun emprunt trouvé.");
        return emprunts;
    }

    public Emprunts GetEmpruntById(int id)
    {
        var emprunt = _empruntsRepo.GetEmprunt(id);
        if (emprunt == null)
            throw new NotFoundException($"Emprunt non trouvé.");
        return emprunt;
    }

    public Emprunts CreateEmprunt(int adherentId, int oeuvreId)
    {
        var adherent = _adherentsRepo.GetAdherent(adherentId);
        if (adherent == null)
            throw new NotFoundException("Adhérent non trouvé.");

        var nbrEmpruntsActifs = _empruntsRepo
            .GetAllEmprunts()
            .Count(e => e.Adherent!.Id == adherentId && e.DateRetourReelle == null);
        if (nbrEmpruntsActifs >= 10)
            throw new LimiteEmpruntsDepasseeException("L'adhérent a déjà 10 emprunts actifs.");

        var exemplaire = _exemplairesRepo
            .GetExemplairesByOeuvre(oeuvreId)
            .FirstOrDefault(e => e.Statut == StatutExemplaire.Disponible);
        if (exemplaire == null)
            throw new DomainValidationException(
                "Aucun exemplaire est disponible pour cette oeuvre."
            );

        var emprunt = new Emprunts
        {
            Adherent = adherent,
            Exemplaire = exemplaire,
            DateEmprunt = DateOnly.FromDateTime(DateTime.Now),
            DateRetourPrevue = DateOnly.FromDateTime(DateTime.Now.AddDays(21)),
            DateRetourReelle = null,
        };
        exemplaire.Statut = StatutExemplaire.Emprunte;

        var reservation = _reservationsRepo
            .GetReservationsByAdherent(adherentId)
            .FirstOrDefault(r => r.Oeuvre!.Id == oeuvreId && r.Statut == StatutReservation.Active);
        if (reservation != null)
        {
            reservation.Statut = StatutReservation.Terminee;
        }

        _empruntsRepo.AddEmprunt(emprunt);
        _empruntsRepo.SaveChanges();

        return emprunt;
    }

    public Emprunts UpdateEmpruntRetour(int id, DateOnly? dateRetour)
    {
        var emprunt = GetEmpruntById(id);

        if (emprunt.DateRetourReelle != null)
            throw new DomainValidationException("Cet emprunt est déjà retourné.");

        bool retourEffectue = emprunt.DateRetourReelle == null && dateRetour != null;
        emprunt.DateRetourReelle = dateRetour;
        if (retourEffectue)
        {
            emprunt.Exemplaire!.Statut = StatutExemplaire.Disponible;
        }
        _empruntsRepo.SaveChanges();

        return emprunt;
    }

    public void DeleteEmprunt(int id)
    {
        var emprunt = _empruntsRepo.GetEmprunt(id);
        if (emprunt == null)
            throw new NotFoundException("Emprunt non trouvé.");

        if (emprunt.DateRetourReelle == null)
        {
            emprunt.Exemplaire!.Statut = StatutExemplaire.Disponible;
        }

        _empruntsRepo.RemoveEmprunt(emprunt);
        _empruntsRepo.SaveChanges();
    }
}

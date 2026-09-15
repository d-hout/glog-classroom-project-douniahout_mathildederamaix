using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReservationsControllerTests
{
    private MockReservationsRepository _mockReservationsRepo = null!;
    private MockAdherentsRepository _mockAdherentRepo = null!;
    private MockOeuvresRepository _mockOeuvreRepo = null!;
    private MockExemplairesRepository _mockExemplairesRepo = null!;
    private ReservationsController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockReservationsRepo = new MockReservationsRepository();
        _mockAdherentRepo = new MockAdherentsRepository();
        _mockOeuvreRepo = new MockOeuvresRepository();
        _mockExemplairesRepo = new MockExemplairesRepository();
        var _service = new ReservationsService(
            _mockReservationsRepo,
            _mockAdherentRepo,
            _mockOeuvreRepo,
            _mockExemplairesRepo
        );
        _controller = new ReservationsController(_service);
    }

    [TestMethod]
    public void Recuperer_reservations_retourne_ok_avec_reservations_actives()
    {
        var result = _controller.RecupererReservations();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<ReservationsDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_reservations_par_adherent_valide_retourne_ok()
    {
        var result = _controller.RecupererReservationsParAdherent(1);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<ReservationsDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(1, dtos.Count());
        Assert.AreEqual(1, dtos.First().AdherentId);
    }

    [TestMethod]
    public void Creer_reservation_valide_retourne_created()
    {
        foreach (var ex in _mockExemplairesRepo.GetExemplairesByOeuvre(1))
            ex.Statut = StatutExemplaire.Emprunte;

        var result = _controller.CreerReservation(1, 1);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);

        var dto = createdResult.Value as ReservationsDto;
        Assert.IsNotNull(dto);

        Assert.AreEqual(1, dto.AdherentId);
        Assert.AreEqual(1, dto.OeuvreId);
    }

    [TestMethod]
    public void Creer_reservation_adherent_inexistant_retourne_not_found()
    {
        var result = _controller.CreerReservation(999, 1);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Creer_reservation_oeuvre_inexistante_retourne_not_found()
    {
        var result = _controller.CreerReservation(1, 999);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Supprimer_reservation_valide_retourne_no_content()
    {
        int validId = 1;
        Assert.IsNotNull(_mockReservationsRepo?.GetReservation(validId));
        var result = _controller.SupprimerReservation(validId);

        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);

        Assert.IsNull(_mockReservationsRepo?.GetReservation(validId));
    }

    [TestMethod]
    public void Supprimer_reservation_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.SupprimerReservation(invalidId);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}

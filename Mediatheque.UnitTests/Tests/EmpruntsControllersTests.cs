using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EmpruntsControllerTests
{
    private MockEmpruntsRepository _mockEmpruntsRepo = null!;
    private MockAdherentsRepository _mockAdherentsRepo = null!;
    private MockExemplairesRepository _mockExemplairesRepo = null!;
    private MockReservationsRepository _mockReservationsRepo = null!;
    private EmpruntsController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockEmpruntsRepo = new MockEmpruntsRepository();
        _mockAdherentsRepo = new MockAdherentsRepository();
        _mockExemplairesRepo = new MockExemplairesRepository();
        _mockReservationsRepo = new MockReservationsRepository();

        var _service = new EmpruntsService(
            _mockEmpruntsRepo,
            _mockAdherentsRepo,
            _mockExemplairesRepo,
            _mockReservationsRepo
        );

        _controller = new EmpruntsController(_service);

        _service.CreateEmprunt(1, 1);
        _service.CreateEmprunt(2, 2);
    }

    [TestMethod]
    public void Recuperer_tous_les_emprunts_retourne_ok_avec_liste()
    {
        var result = _controller.RecupererTousLesEmprunts();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<EmpruntsDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
    }

    [TestMethod]
    public void Recuperer_emprunt_avec_id_valide_retourne_ok()
    {
        int validId = 1;
        var result = _controller.RecupererEmprunt(validId);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as EmpruntsDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(validId, dto.Id);
    }

    [TestMethod]
    public void Recuperer_emprunt_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.RecupererEmprunt(invalidId);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Ajouter_emprunt_avec_id_valide_retourne_created()
    {
        // Utiliser une oeuvre avec un exemplaire disponible après l'initialisation
        var result = _controller.AjouterEmprunt(1, 3);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);

        var dto = createdResult.Value as EmpruntsDto;
        Assert.IsNotNull(dto);
        // L'adherent doit être renseigné et l'exemplaire doit correspondre à l'oeuvre 3
        Assert.IsFalse(string.IsNullOrEmpty(dto.Adherent));
        Assert.AreEqual(5, dto.ExemplaireId);
    }

    [TestMethod]
    public void Ajouter_emprunt_avec_id_invalide_retourne_bad_request()
    {
        var result = _controller.AjouterEmprunt(0, -1);
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
    }

    [TestMethod]
    public void Modifier_emprunt_avec_id_valide_retourne_ok()
    {
        var emprunt = _mockEmpruntsRepo.GetAllEmprunts().First();
        var newDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7));

        var result = _controller.ModifierEmprunt(emprunt.Id, newDate);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as EmpruntsDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(newDate, dto.DateRetourReelle);
    }

    [TestMethod]
    public void Modifier_emprunt_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.ModifierEmprunt(999, DateOnly.FromDateTime(DateTime.Today));
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Supprimer_emprunt_avec_id_valide_retourne_no_content()
    {
        int validId = 1;
        Assert.IsNotNull(_mockEmpruntsRepo?.GetEmprunt(validId));
        var result = _controller.SupprimerEmprunt(validId);

        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);

        Assert.IsNull(_mockEmpruntsRepo?.GetEmprunt(validId));
    }

    [TestMethod]
    public void Supprimer_emprunt_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.SupprimerEmprunt(invalidId);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}

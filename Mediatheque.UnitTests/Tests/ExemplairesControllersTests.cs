using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ExemplairesControllerTests
{
    private MockExemplairesRepository _mockExemplairesRepo = null!;
    private MockOeuvresRepository _mockOeuvreRepo = null!;
    private ExemplairesController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockExemplairesRepo = new MockExemplairesRepository();
        _mockOeuvreRepo = new MockOeuvresRepository();
        var _service = new ExemplairesService(_mockExemplairesRepo, _mockOeuvreRepo);
        _controller = new ExemplairesController(_service);
    }

    [TestMethod]
    public void Recuperer_exemplaires_par_oeuvre_existante_retourne_ok()
    {
        int validIdOeuvre = 1;
        var result = _controller.RecupererExemplairesParOeuvre(validIdOeuvre);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<ExemplairesDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
        Assert.IsTrue(dtos.All(d => d.OeuvreTitre == "Livre A"));
    }

    [TestMethod]
    public void Recuperer_exemplaires_par_oeuvre_inexistante_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.RecupererExemplairesParOeuvre(invalidId);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Recuperer_exemplaire_avec_id_valide_retourne_ok()
    {
        int validId = 1;
        var exemplaire = _mockExemplairesRepo.GetExemplaire(validId);
        var result = _controller.RecupererExemplaire(validId);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as ExemplairesDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(exemplaire!.Id, dto.Id);
        Assert.AreEqual(exemplaire.Oeuvre?.Titre, dto.OeuvreTitre);
        Assert.AreEqual(exemplaire.Statut.ToString(), dto.Statut);
    }

    [TestMethod]
    public void Recuperer_exemplaire_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.RecupererExemplaire(invalidId);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Ajouter_exemplaire_avec_id_valide_retourne_created()
    {
        var result = _controller.AjouterExemplaire(2);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);

        var dto = createdResult.Value as ExemplairesDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Livre B", dto.OeuvreTitre);

        Assert.IsNotNull(_mockExemplairesRepo.GetExemplaire(dto.Id));
    }

    [TestMethod]
    public void Modifier_exemplaire_avce_id_valide_retourne_ok()
    {
        var result = _controller.ModifierExemplaire(1, 2);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as ExemplairesDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Livre B", dto.OeuvreTitre);
    }

    [TestMethod]
    public void Modifier_exemplaire_avec_id_invalide_retourne_not_found()
    {
        var result = _controller.ModifierExemplaire(999, 1);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Supprimer_exemplaire_avec_id_valide_Retourne_no_content()
    {
        int validId = 1;
        Assert.IsNotNull(_mockExemplairesRepo?.GetExemplaire(validId));
        var result = _controller.SupprimerExemplaire(validId);

        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);

        Assert.IsNull(_mockExemplairesRepo?.GetExemplaire(validId));
    }

    [TestMethod]
    public void SupprimerExemplaire_Invalide_RetourneNotFound()
    {
        int invalidId = 999;
        var result = _controller.SupprimerExemplaire(invalidId);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}

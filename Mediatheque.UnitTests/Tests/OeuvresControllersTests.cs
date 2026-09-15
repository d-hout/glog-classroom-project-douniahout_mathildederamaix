using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class OeuvresControllerTests
{
    private MockOeuvresRepository _mockRepo = null!;
    private OeuvresController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new MockOeuvresRepository();
        var _service = new OeuvresService(_mockRepo);
        _controller = new OeuvresController(_service);

        _mockRepo.AddOeuvre(
            new Oeuvres
            {
                Titre = "Candide",
                Type = OeuvreType.Livre,
                Auteurs = _mockRepo.GetAuteur(1),
            }
        );
    }

    [TestMethod]
    public void Recuperer_toutes_les_oeuvres_retourne_ok_avec_liste()
    {
        var result = _controller.RecupererOeuvres();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<OeuvresDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(_mockRepo.GetAllOeuvres().Count(), dtos.Count());
    }

    [TestMethod]
    public void Recuperer_oeuvre_avec_id_valide_retourne_ok()
    {
        // Récupérer l'oeuvre qui vient d'être ajoutée dans Setup
        var addedOeuvre = _mockRepo.GetAllOeuvres().Last();
        int validId = addedOeuvre.Id;
        var result = _controller.RecupererOeuvre(validId);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as OeuvresDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(validId, dto.Id);
        Assert.AreEqual("Candide", dto.Titre);
    }

    [TestMethod]
    public void Recuperer_oeuvre_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.RecupererOeuvre(invalidId);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Ajouter_oeuvre_avec_id_valide_retourne_created()
    {
        var result = _controller.AjouterOeuvre("Le Petit Prince", OeuvreType.Livre, 2);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);

        var dto = createdResult.Value as OeuvresDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual("Le Petit Prince", dto.Titre);
        Assert.AreEqual("Albert Camus", dto.Auteur); // auteur 2
    }

    [TestMethod]
    public void Ajouter_oeuvre_avec_auteur_invalide_retourne_not_found()
    {
        var result = _controller.AjouterOeuvre("Inconnu", OeuvreType.Livre, 999);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Supprimer_oeuvre_avec_id_valide_retourne_no_content()
    {
        int validId = 1;
        Assert.IsNotNull(_mockRepo?.GetOeuvre(validId));
        var result = _controller.SupprimerOeuvre(validId);

        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);

        Assert.IsNull(_mockRepo?.GetOeuvre(validId));
    }

    [TestMethod]
    public void Supprimer_oeuvre_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.SupprimerOeuvre(invalidId);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}

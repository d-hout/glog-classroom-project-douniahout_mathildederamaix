using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AdherentsControllerTests
{
    private MockAdherentsRepository? _mockRepo;
    private AdherentsController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new MockAdherentsRepository();
        var _service = new AdherentsService(_mockRepo);
        _controller = new AdherentsController(_service);
    }

    [TestMethod]
    public void Recuperer_adherents_retourne_ok_avec_liste()
    {
        var result = _controller.RecupererAdherents();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<AdherentsDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());

        var nomsAttendus = new[] { "Durand", "Martin" };
        var nomsRecus = dtos.Select(a => a.Nom).ToArray();
        CollectionAssert.AreEquivalent(nomsAttendus, nomsRecus);
    }

    [TestMethod]
    public void Recuperer_adherent_avec_id_valide_retourne_ok()
    {
        int validId = 1;
        var result = _controller.RecupererAdherent(validId);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AdherentsDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(validId, dto.Id);
        Assert.AreEqual("Durand", dto.Nom);
    }

    [TestMethod]
    public void Recuperer_adherent_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.RecupererAdherent(invalidId);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Ajouter_adherent_retourne_created()
    {
        string nom = "Dupont";
        string prenom = "Louis";
        string email = "louis.dupont@email.com";
        var result = _controller.AjouterAdherent(nom, prenom, email);

        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);

        var dto = createdResult.Value as AdherentsDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(nom, dto.Nom);

        var adherentDansMock = _mockRepo?.GetAdherent(3);
        Assert.IsNotNull(adherentDansMock);
        Assert.AreEqual(prenom, adherentDansMock.Prenom);
    }

    [TestMethod]
    public void Ajouter_adherent_avec_nom_prenom_email_vide_retourne_bad_request()
    {
        string nomVide = " ";
        string prenomVide = " ";
        string emailVide = " ";
        var result = _controller.AjouterAdherent(nomVide, prenomVide, emailVide);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(
            "Ni le nom, ni le prénom et ni l'email ne peuvent pas être vides.",
            badRequestResult.Value
        );
    }

    [TestMethod]
    public void Modifier_adherent_avec_id_valide_retourne_ok()
    {
        int validId = 1;
        string nouveauNom = "Nom mis à jour";
        string nouveauPrenom = "Prenom mis à jour";
        string nouveauEmail = "Email mis à jour";
        var result = _controller.ModifierAdherent(validId, nouveauNom, nouveauPrenom, nouveauEmail);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AdherentsDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(nouveauNom, dto.Nom);

        var adherentDansMock = _mockRepo?.GetAdherent(validId);
        Assert.IsNotNull(adherentDansMock);
        Assert.AreEqual(nouveauPrenom, adherentDansMock?.Prenom);
    }

    [TestMethod]
    public void Modifier_adherent_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.ModifierAdherent(invalidId, "Nom", "Prenom", "email@test.com");

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Modifier_adherent_avec_nom_prenom_email_vide_retourne_bad_request()
    {
        int validId = 1;
        string nomVide = null!;
        string prenomVide = null!;
        string emailVide = null!;
        var result = _controller.ModifierAdherent(validId, nomVide, prenomVide, emailVide);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(
            "Le nom, le prénom et l'email ne peuvent pas être vides.",
            badRequestResult.Value
        );
    }

    [TestMethod]
    public void Supprimer_adherent_avec_id_valide_retourne_no_content()
    {
        int validId = 1;
        Assert.IsNotNull(_mockRepo?.GetAdherent(validId));
        var result = _controller.SupprimerAdherent(validId);

        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);

        Assert.IsNull(_mockRepo?.GetAdherent(validId));
    }

    [TestMethod]
    public void Supprimer_adherent_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.SupprimerAdherent(invalidId);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}

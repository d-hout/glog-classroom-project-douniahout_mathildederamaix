using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AuteursControllerTests
{
    private MockAuteursRepository _mockRepo = null!;
    private AuteursController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new MockAuteursRepository();
        var _service = new AuteursService(_mockRepo);
        _controller = new AuteursController(_service);
    }

    [TestMethod]
    public void Recuperer_auteurs_retourne_ok_avec_liste()
    {
        var result = _controller.RecupererAuteurs();

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dtos = okResult.Value as IEnumerable<AuteursDto>;
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count());
        var nomsAttendus = new[] { "Hugo", "Camus" };
        var nomsRecus = dtos.Select(b => b.Nom).ToList();
        CollectionAssert.AreEquivalent(nomsAttendus, nomsRecus);
    }

    [TestMethod]
    public void Recuperer_auteur_avec_id_valide_retourne_ok()
    {
        var validId = 2;
        var result = _controller.RecupererAuteur(validId);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AuteursDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(validId, dto.Id);
        Assert.AreEqual("Camus", dto.Nom);
    }

    [TestMethod]
    public void Recuperer_auteur_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.RecupererAuteur(invalidId);

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Ajouter_auteur_retourne_created()
    {
        string nom = "Zola";
        string prenom = "Emile";
        var result = _controller.AjouterAuteur(nom, prenom);

        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);

        var dto = createdResult.Value as AuteursDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(nom, dto.Nom);
        Assert.AreEqual(prenom, dto.Prenom);

        var auteurDansMock = _mockRepo.GetAuteur(3);
        Assert.IsNotNull(auteurDansMock);
        Assert.AreEqual(nom, auteurDansMock.Nom);
        Assert.AreEqual(prenom, auteurDansMock.Prenom);
    }

    [TestMethod]
    public void Ajouter_auteur_avec_nom_prenom_vide_retourne_bad_request()
    {
        string nomVide = " ";
        string prenomVide = " ";
        var result = _controller.AjouterAuteur(nomVide, prenomVide);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom et le prénom ne peuvent pas être vides.", badRequestResult.Value);
    }

    [TestMethod]
    public void Modifier_auteur_avec_id_valide_retourne_ok()
    {
        int validId = 1;
        string nouveauNom = "Nom mis à jour";
        string nouveauPrenom = "Prenom mis à jour";
        var result = _controller.ModifierAuteur(validId, nouveauNom, nouveauPrenom);

        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var dto = okResult.Value as AuteursDto;
        Assert.IsNotNull(dto);
        Assert.AreEqual(nouveauNom, dto.Nom);
        Assert.AreEqual(nouveauPrenom, dto.Prenom);

        var auteurDansMock = _mockRepo?.GetAuteur(validId);
        Assert.IsNotNull(auteurDansMock);
        Assert.AreEqual(nouveauNom, auteurDansMock?.Nom);
        Assert.AreEqual(nouveauPrenom, auteurDansMock?.Prenom);
    }

    [TestMethod]
    public void Modifier_auteur_avec_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.ModifierAuteur(invalidId, "Nom", "Prenom");

        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public void Modifier_auteur_avec_nom_prenom_vide_retourne_bad_request()
    {
        int validId = 1;
        string nomVide = null!;
        string prenomVide = null!;
        var result = _controller.ModifierAuteur(validId, nomVide, prenomVide);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Le nom et le prénom ne peuvent pas être vides.", badRequestResult.Value);
    }

    [TestMethod]
    public void Supprimer_auteur_avec_id_valide_retourne_no_content()
    {
        int validId = 1;
        Assert.IsNotNull(_mockRepo?.GetAuteur(validId));
        var result = _controller.SupprimerAuteur(validId);

        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);

        Assert.IsNull(_mockRepo?.GetAuteur(validId));
    }

    [TestMethod]
    public void Supprimer_auteur_avce_id_invalide_retourne_not_found()
    {
        int invalidId = 999;
        var result = _controller.SupprimerAuteur(invalidId);

        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
    }
}

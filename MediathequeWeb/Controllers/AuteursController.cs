using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auteurs")]
public class AuteursController : ControllerBase
{
    private readonly AuteursService _service;

    public AuteursController(AuteursService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AuteursDto>> RecupererAuteurs()
    {
        var auteurs = _service.GetAllAuteurs();
        return Ok(auteurs.Select(AuteursDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<AuteursDto> RecupererAuteur(int id)
    {
        try
        {
            var auteur = _service.GetAuteurById(id);
            return Ok(AuteursDto.FromModel(auteur!));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<AuteursDto> AjouterAuteur(string nom, string prenom)
    {
        try
        {
            var auteur = _service.CreateAuteur(nom, prenom);
            return CreatedAtAction(
                nameof(RecupererAuteur),
                new { id = auteur.Id },
                AuteursDto.FromModel(auteur)
            );
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<AuteursDto> ModifierAuteur(int id, string nom, string prenom)
    {
        try
        {
            var auteur = _service.UpdateAuteur(id, nom, prenom);
            return Ok(AuteursDto.FromModel(auteur!));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult SupprimerAuteur(int id)
    {
        try
        {
            _service.DeleteAuteur(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

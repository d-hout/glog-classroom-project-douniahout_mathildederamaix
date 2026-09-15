using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/adherent")]
public class AdherentsController : ControllerBase
{
    private readonly AdherentsService _service;

    public AdherentsController(AdherentsService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AdherentsDto>> RecupererAdherents()
    {
        try
        {
            var adherents = _service.GetAllAdherents();
            return Ok(adherents.Select(AdherentsDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<AdherentsDto> RecupererAdherent(int id)
    {
        try
        {
            var adherent = _service.GetAdherentById(id);
            return Ok(AdherentsDto.FromModel(adherent));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<AdherentsDto> AjouterAdherent(string nom, string prenom, string email)
    {
        try
        {
            var adherent = _service.CreateAdherent(nom, prenom, email);
            return CreatedAtAction(
                nameof(RecupererAdherent),
                new { id = adherent.Id },
                AdherentsDto.FromModel(adherent)
            );
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<AdherentsDto> ModifierAdherent(
        int id,
        string nom,
        string prenom,
        string email
    )
    {
        try
        {
            var adherent = _service.UpdateAdherent(id, nom, prenom, email);
            return Ok(AdherentsDto.FromModel(adherent));
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
    public ActionResult SupprimerAdherent(int id)
    {
        try
        {
            _service.DeleteAdherent(id);
            return NoContent();
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
}

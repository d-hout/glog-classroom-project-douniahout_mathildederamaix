using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ExemplairesController : ControllerBase
{
    private readonly ExemplairesService _service;

    public ExemplairesController(ExemplairesService service)
    {
        _service = service;
    }

    [HttpGet("oeuvre/{id}/exemplaire")]
    public ActionResult<IEnumerable<ExemplairesDto>> RecupererExemplairesParOeuvre(int id)
    {
        try
        {
            var exemplaires = _service.GetAllExemplairesByOeuvre(id);
            return Ok(exemplaires.Select(ExemplairesDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("exemplaire/{id}")]
    public ActionResult<ExemplairesDto> RecupererExemplaire(int id)
    {
        try
        {
            var exemplaire = _service.GetExemplaireById(id);
            return Ok(ExemplairesDto.FromModel(exemplaire));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("oeuvre/{id}/exemplaire")]
    public ActionResult<ExemplairesDto> AjouterExemplaire(int id)
    {
        try
        {
            var exemplaire = _service.CreateExemplaire(id);
            return CreatedAtAction(
                nameof(RecupererExemplaire),
                new { id = exemplaire!.Id },
                ExemplairesDto.FromModel(exemplaire)
            );
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("exemplaire/{id}")]
    public ActionResult<ExemplairesDto> ModifierExemplaire(int id, int? nouveauOeuvreId = null)
    {
        try
        {
            var exemplaire = _service.UpdateExemplaire(id, nouveauOeuvreId);
            return Ok(ExemplairesDto.FromModel(exemplaire));
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

    [HttpDelete("exemplaire/{id}")]
    public ActionResult SupprimerExemplaire(int id)
    {
        try
        {
            _service.DeleteExemplaire(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

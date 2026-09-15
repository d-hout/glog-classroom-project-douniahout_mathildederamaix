using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/oeuvre")]
public class OeuvresController : ControllerBase
{
    private readonly OeuvresService _service;

    public OeuvresController(OeuvresService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<OeuvresDto>> RecupererOeuvres()
    {
        var oeuvres = _service.GetAllOeuvres();
        return Ok(oeuvres.Select(OeuvresDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<OeuvresDto> RecupererOeuvre(int id)
    {
        try
        {
            var oeuvre = _service.GetOeuvreById(id);
            return Ok(OeuvresDto.FromModel(oeuvre));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<OeuvresDto> AjouterOeuvre(string titre, OeuvreType type, int auteurId)
    {
        try
        {
            var oeuvre = _service.CreateOeuvre(titre, type, auteurId);
            return CreatedAtAction(
                nameof(RecupererOeuvre),
                new { id = oeuvre.Id },
                OeuvresDto.FromModel(oeuvre)
            );
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

    [HttpPut("{id}")]
    public ActionResult<OeuvresDto> ModifierOeuvre(int id, string titre, OeuvreType type)
    {
        try
        {
            var oeuvre = _service.UpdateOeuvre(id, titre, type);
            return Ok(OeuvresDto.FromModel(oeuvre));
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
    public ActionResult SupprimerOeuvre(int id)
    {
        try
        {
            _service.DeleteOeuvre(id);
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

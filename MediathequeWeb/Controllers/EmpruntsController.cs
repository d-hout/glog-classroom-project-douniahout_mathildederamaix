using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/emprunt")]
public class EmpruntsController : ControllerBase
{
    private readonly EmpruntsService _service;

    public EmpruntsController(EmpruntsService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmpruntsDto>> RecupererTousLesEmprunts()
    {
        var emprunts = _service.GetAllEmprunts();
        return Ok(emprunts.Select(EmpruntsDto.FromModel));
    }

    [HttpGet("{id}")]
    public ActionResult<EmpruntsDto> RecupererEmprunt(int id)
    {
        try
        {
            var emprunt = _service.GetEmpruntById(id);
            return Ok(EmpruntsDto.FromModel(emprunt));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<EmpruntsDto> AjouterEmprunt(int adherentId, int oeuvreId)
    {
        try
        {
            var emprunt = _service.CreateEmprunt(adherentId, oeuvreId);
            return CreatedAtAction(
                nameof(RecupererEmprunt),
                new { id = emprunt.Id },
                EmpruntsDto.FromModel(emprunt)
            );
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DomainValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<EmpruntsDto> ModifierEmprunt(int id, DateOnly? dateRetour)
    {
        try
        {
            var emprunt = _service.UpdateEmpruntRetour(id, dateRetour);
            return Ok(EmpruntsDto.FromModel(emprunt));
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
    public ActionResult SupprimerEmprunt(int id)
    {
        try
        {
            _service.DeleteEmprunt(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationsService _service;

    public ReservationsController(ReservationsService service)
    {
        _service = service;
    }

    [HttpGet("reservation")]
    public ActionResult<IEnumerable<ReservationsDto>> RecupererReservations()
    {
        var reservations = _service.GetAllReservationsActive();
        return Ok(reservations.Select(ReservationsDto.FromModel));
    }

    [HttpGet("adherent/{id}/reservation")]
    public ActionResult<IEnumerable<ReservationsDto>> RecupererReservationsParAdherent(int id)
    {
        try
        {
            var reservations = _service.GetReservationsByAdherent(id);
            return Ok(reservations.Select(ReservationsDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("oeuvre/{id}/reservation")]
    public ActionResult<IEnumerable<ReservationsDto>> RecupererReservationsParOeuvre(int id)
    {
        try
        {
            var reservations = _service.GetReservationsByOeuvre(id);
            return Ok(reservations.Select(ReservationsDto.FromModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("reservation")]
    public ActionResult<ReservationsDto> CreerReservation(int adherentId, int oeuvreId)
    {
        try
        {
            var reservation = _service.CreateReservation(adherentId, oeuvreId);
            return CreatedAtAction(
                nameof(RecupererReservations),
                new { id = reservation.Id },
                ReservationsDto.FromModel(reservation)
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

    [HttpDelete("/reservation/{id}")]
    public ActionResult SupprimerReservation(int id)
    {
        try
        {
            _service.DeleteReservation(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

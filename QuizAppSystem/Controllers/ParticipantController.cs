// ParticipantController.cs
using Microsoft.AspNetCore.Mvc;
using QuizAppSystem.Models;
using QuizAppSystem.Service.Interface;
using QuizAppSystem.DTO;

[ApiController]
[Route("api/[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantController(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    [HttpGet]
    public IActionResult GetAllParticipants()
    {
        var participants = _participantService.GetAllParticipants();
        return Ok(participants);
    }

    [HttpGet("{id}")]
    public IActionResult GetParticipant(Guid id)
    {
        var participant = _participantService.GetParticipantById(id);

        if (participant != null)
        {
            return Ok(participant);
        }

        return NotFound("Participant not found.");
    }
    // ParticipantController.cs
    [HttpPost]
    public IActionResult CreateParticipant([FromBody] ParticipantDTO participantDTO)
    {
        if (participantDTO != null)
        {
            // Ensure ID is not set manually
            participantDTO.Id = default(Guid);

            // The service will handle the creation of the participant with an automatically generated ID
            _participantService.CreateParticipant(participantDTO);

            // Return the response without the "id" property
            return CreatedAtAction("GetParticipant", new { id = participantDTO.Id }, new
            {
                participantDTO.FirstName,
                participantDTO.LastName,
                participantDTO.Email,
                participantDTO.RegistrationDate,
                participantDTO.StartedAt,
                participantDTO.CompletedAt,
                participantDTO.IsSuccessful,
                participantDTO.IsCompleted,
                participantDTO.PIN
            });
        }

        return BadRequest("Invalid participant data.");
    }



    [HttpPut("{id}")]
    public IActionResult UpdateParticipant(Guid id, [FromBody] ParticipantDTO updatedParticipantDTO)
    {
        _participantService.UpdateParticipant(id, updatedParticipantDTO);
        return Ok("Participant updated successfully.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteParticipant(Guid id)
    {
        _participantService.DeleteParticipant(id);
        return Ok("Participant deleted successfully.");
    }
}

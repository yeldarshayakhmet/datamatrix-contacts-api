using Core.Errors;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Requests;

namespace Presentation.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> GetContacts([FromQuery] GetContactListRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _contactService.GetContactsList(request, cancellationToken);
        if (result.Errors is null || !result.Errors.Any())
        {
            return Ok(result);
        }

        if (result.Errors.First() is EntityNotFoundError)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContact([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _contactService.GetContact(id, cancellationToken);
        if (result.Errors is null || !result.Errors.Any())
        {
            return Ok(result);
        }

        if (result.Errors.First() is EntityNotFoundError)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(
        [FromBody] CreateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _contactService.CreateContact(request, cancellationToken);
        if (result.Errors is null || !result.Errors.Any())
        {
            return CreatedAtAction(nameof(CreateContact), result);
        }

        return BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact(
        [FromBody] UpdateContactRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _contactService.UpdateContact(request, cancellationToken);
        if (result.Errors is null || !result.Errors.Any())
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteContact([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var result = await _contactService.DeleteContact(id, cancellationToken);
        if (result.Errors is null || !result.Errors.Any())
        {
            return Ok(result);
        }

        if (result.Errors.First() is EntityNotFoundError)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }
}
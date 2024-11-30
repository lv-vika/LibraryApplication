using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService authorService;

    public AuthorController(IAuthorService authorService)
    {
        this.authorService = authorService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<AuthorModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this.authorService.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AuthorModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await this.authorService.GetById(id));
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] AuthorModel authorModel)
    {
        var id = await this.authorService.Create(authorModel);
        return Ok(id);
    }

    [HttpPut("{id:int}/edit")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int id, [FromBody] AuthorModel authorModel)
    {
        var updated = await this.authorService.Update(id, authorModel);
        return Ok(updated);
    }
    
    [HttpDelete("{id:int}/delete")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await this.authorService.Delete(id);
        return Ok(deleted);
    }
}
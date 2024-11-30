using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferTypeController : ControllerBase
{
    private readonly ITransferTypeService transferTypeService;

    public TransferTypeController(ITransferTypeService transferTypeService)
    {
        this.transferTypeService = transferTypeService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<TransferTypeModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this.transferTypeService.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TransferTypeModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await this.transferTypeService.GetById(id));
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TransferTypeModel transferTypeModel)
    {
        var id = await this.transferTypeService.Create(transferTypeModel);
        return Ok(id);
    }

    [HttpPut("{id:int}/edit")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int id, [FromBody] TransferTypeModel transferTypeModel)
    {
        var updated = await this.transferTypeService.Update(id, transferTypeModel);
        return Ok(updated);
    }
    
    [HttpDelete("{id:int}/delete")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await this.transferTypeService.Delete(id);
        return Ok(deleted);
    }
}
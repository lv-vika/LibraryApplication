using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService discountService;

    public DiscountController(IDiscountService discountService)
    {
        this.discountService = discountService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<DiscountModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this.discountService.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DiscountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await this.discountService.GetById(id));
    }

    [HttpGet]
    [ProducesResponseType(typeof(DiscountModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromQuery] string name)
    {
        var discount = await this.discountService.GetByName(name);
        return discount is null ? NotFound() : Ok(discount);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] DiscountModel bookModel)
    {
        var id = await this.discountService.Create(bookModel);
        return Ok(id);
    }

    [HttpPut("{id:int}/edit")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int id, [FromBody] DiscountModel bookModel)
    {
        var updated = await this.discountService.Update(id, bookModel);
        return Ok(updated);
    }
    
    [HttpDelete("{id:int}/delete")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await this.discountService.Delete(id);
        return Ok(deleted);
    }
}
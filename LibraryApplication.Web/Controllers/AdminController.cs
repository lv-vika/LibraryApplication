using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService adminService;

    public AdminController(IAdminService adminService)
    {
        this.adminService = adminService;
    }

        [HttpPost("generate-past-fines")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateFines([FromQuery] int amount)
    {
        await this.adminService.GenerateFinesPastDueDate(amount);
        return Ok();
    }

    [HttpPost("generate-damage-fines")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateFineForBookDamage([FromBody] FineModel fineModel)
    {
        await this.adminService.GenerateFineForBookDamage(fineModel.BookId, fineModel.Amount);
        return Ok();
    }
}
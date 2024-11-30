using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCategoryController : ControllerBase
{
    private readonly IUserCategoryService userCategoryService;

    public UserCategoryController(IUserCategoryService userCategoryService)
    {
        this.userCategoryService = userCategoryService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<UserCategoryModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this.userCategoryService.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserCategoryModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await this.userCategoryService.GetById(id));
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] UserCategoryModel userCategoryModel)
    {
        var id = await this.userCategoryService.Create(userCategoryModel);
        return Ok(id);
    }

    [HttpPut("{id:int}/edit")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int id, [FromBody] UserCategoryModel userCategoryModel)
    {
        var updated = await this.userCategoryService.Update(id, userCategoryModel);
        return Ok(updated);
    }
    
    [HttpDelete("{id:int}/delete")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await this.userCategoryService.Delete(id);
        return Ok(deleted);
    }
}
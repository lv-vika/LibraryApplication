using LibraryApplication.Attributes;
using LibraryApplication.Data.Interfaces.Services;
using LibraryApplication.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService bookService;

    public BookController(IBookService bookService)
    {
        this.bookService = bookService;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await this.bookService.GetAll());
    }

    [HttpGet("{id:int}")]
    [ExistingBook]
    [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await this.bookService.GetById(id));
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] BookModel bookModel)
    {
        var id = await this.bookService.Create(bookModel);
        return Ok(id);
    }

    [HttpPut("{id:int}/edit")]
    [ExistingBook]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(int id, [FromBody] BookModel bookModel)
    {
        var updated = await this.bookService.Update(id, bookModel);
        return Ok(updated);
    }
    
    [HttpDelete("{id:int}/delete")]
    [ExistingBook]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await this.bookService.Delete(id);
        return Ok(deleted);
    }

    [HttpPost("{id:int}/borrow")]
    [ExistingBook]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BorrowBook(int id, [FromBody] BorrowBookModel borrowBookModel)
    {
        return Ok(await this.bookService.TryBorrowBook(id, borrowBookModel));
    }
    
    [HttpPost("{id:int}/return")]
    [ExistingBook]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReturnBook(int id, [FromQuery] int userId)
    {
        return Ok(await this.bookService.TryReturnBook(id, userId));
    }

    [HttpGet("available")]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableBooks()
    {
        return Ok(await this.bookService.GetAvailableBooks());
    }
}
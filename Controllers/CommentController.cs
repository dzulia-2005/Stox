using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Interfaces.CommentRepo;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Controllers;

[Route("api/comments")]
[ApiController]

public class CommentController : ControllerBase
{
    
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;
    
    public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo)
    {
        _commentRepo = commentRepo;
        _stockRepo = stockRepo;
    }

    [HttpGet]
    public async Task<IActionResult> getAllComments()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comments = await _commentRepo.GetAllAsync();
        var commentsDto = comments.Select(s => s.ToCommentDto());

        return Ok(commentsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> getById([FromRoute] int id)
    {
        var comment = await _commentRepo.GetByIdAsync(id);
        if (comment==null)
        {
            return NotFound();
        }

        return Ok(comment.ToCommentDto());
    }
    

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> AddCommentAsync([FromRoute] int stockId,CreateCommentDto commentDto)
    {
        if (!await _stockRepo.StockExists(stockId))
        {
            return BadRequest("stock does not exist");
        }

        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(getById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> UpdateCommentAsync([FromRoute] int Id,[FromBody] UpdateCommentRequestDto commentRequestDto)
    {
        var comment = await _commentRepo.UpdateAsync(Id, commentRequestDto.ToCommentFromUpdate());

        if (comment == null)
        {
            return NotFound("comment not found");
        }

        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> DeleteCommentAsync([FromRoute] int Id)
    {
        var comment = await _commentRepo.DeleteAsync(Id);
        if (comment == null)
        {
            return NotFound("comment not found");
        }

        return NoContent();
    }
}
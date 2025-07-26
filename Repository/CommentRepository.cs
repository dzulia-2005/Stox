using api.Data;
using api.Dtos.Comment;
using api.Interfaces.CommentRepo;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment?> DeleteByIdAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int Id,Comment comment)
    {
        var existingComment = await _context.Comments.FindAsync(Id);
        if (existingComment == null)
        {
            return null;
        }

        existingComment.Title = comment.Title;
        existingComment.Content = comment.Content;

        await _context.SaveChangesAsync();
        return existingComment;
    }


}
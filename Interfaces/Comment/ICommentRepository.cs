using api.Models;

namespace api.Interfaces.CommentRepo;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment?> DeleteByIdAsync(int id);
    Task<Comment?> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int Id,Comment comment);
}
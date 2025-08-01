using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5,ErrorMessage = "name must be at least 5 characters")]
    [MaxLength(280,ErrorMessage = "name cannot exceed 280 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5,ErrorMessage = "name must be at least 5 characters")]
    [MaxLength(280,ErrorMessage = "name cannot exceed 280 characters")]
    public string Content { get; set; } = string.Empty;
}
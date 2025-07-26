using api.Dtos.Comment;
using api.Models;

namespace api.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            Date = commentModel.Date,

        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentDto commentDto,int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId
        };
    }

    public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentRequestDto)
    {
        return new Comment
        {
            Title = commentRequestDto.Title,
            Content = commentRequestDto.Content,
        };
    }
}
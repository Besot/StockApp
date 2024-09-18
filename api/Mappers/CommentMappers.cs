
using api.Dtos.Comment;
using api.Models;
using api.Dtos;
namespace api.Mappers
{
    public static class CommentMappers
    {
         public static CommentDto ToCommentDto(this Comment  comment)
        {
            return new CommentDto
           {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                Likes = comment.Likes
            };
        }

        public static Comment ToCommentFromCreateCommentDto(this  CreateCommentRequestDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = commentDto.StockId
            };
        }
    }
}

using api.Dtos.Comment;
using api.Models;
using api.Dtos;
namespace api.Mappers
{
    public static class CommentMappers
    {
         public static CommentDto ToCommentDto(this Comment  commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId 

            };  
        }

        public static Comment ToCommentFromCreateCommentDto(this  CreateCommentRequestDto commentDto)
        {
            return new Comment
            {
                Id = commentDto.Id,
                Title = commentDto.Title,
                Content = commentDto.Content,
                CreatedOn = commentDto.CreatedOn,
                StockId = commentDto.StockId
            };
        }
    }
}
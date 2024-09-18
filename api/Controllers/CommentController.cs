using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController] 
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepo;
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepo)
        {
            _context = context;
            _commentRepo = commentRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDtos = comments.Select(s => s.ToCommentDto()).ToList();
            return Ok(commentDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentCreateDto)
        {
            var comment = commentCreateDto.ToCommentFromCreateCommentDto();
            var createdComment = await _commentRepo.CreateAsync(comment);
            return Ok(createdComment.ToCommentDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentRepo.DeleteAsync(id);
            if (!result) return NotFound("Comment not found.");
            return Ok("Comment deleted successfully.");
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> Like(int id)
        {
            var result = await _commentRepo.LikeAsync(id);
            if (!result) return NotFound("Comment not found or could not be liked.");
            return Ok("Comment liked successfully.");
        }

        [HttpPost("{id}/unlike")]
        public async Task<IActionResult> Unlike(int id)
        {
            var result = await _commentRepo.UnlikeAsync(id);
            if (!result) return NotFound("Comment not found or could not be unliked.");
            return Ok("Comment unliked successfully.");
        }
    }
}
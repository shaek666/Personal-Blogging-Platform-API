using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBloggingPlatform.Data;
using PersonalBloggingPlatform.DTOs;
using PersonalBloggingPlatform.Models;

namespace PersonalBloggingPlatform.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    public PostsController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] string? term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            var posts = await _appDbContext.Posts
                .AsNoTracking()
                .ToListAsync();
            return Ok(posts);
        }

        else
        {
            var pattern = $"%{term}%";

            var posts = await _appDbContext.Posts
                .Where(p => 
                    EF.Functions.ILike(p.Title, pattern) || 
                    EF.Functions.ILike(p.Content, pattern) ||
                    (p.Category != null && 
                        EF.Functions.ILike(p.Category, pattern)))
                .AsNoTracking()
                .ToListAsync();
            return Ok(posts);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Post>> GetPost(Guid id)
    {
        var post = await _appDbContext.Posts.FindAsync(id);
        if (post is null) return NotFound();
        return post;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> Create([FromBody] CreatePostRequest request)
    {
        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            Category = request.Category ?? string.Empty,
            Tags = request.Tags ?? new List<string>(),
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _appDbContext.Posts.Add(post);
        await _appDbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPost), new { id = post.Id}, post);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Post>> Update(Guid id, [FromBody] UpdatePostRequest request)
    {
        var Post = await _appDbContext.Posts.FindAsync(id);
        if (Post is null) return NotFound();
        Post.Title = request.Title;
        Post.Content = request.Content;
        Post.Category = request.Category ?? string.Empty;
        Post.Tags = request.Tags ?? new List<string>();
        Post.UpdatedAt = DateTime.UtcNow;
        await _appDbContext.SaveChangesAsync();
        return Ok(Post);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var post = await _appDbContext.Posts.FindAsync(id);
        if (post is null) return NotFound();
        _appDbContext.Posts.Remove(post);
        await _appDbContext.SaveChangesAsync();
        return NoContent();
    }
}
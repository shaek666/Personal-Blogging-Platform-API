using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBloggingPlatform.Models;
public class Post
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [Required, Column(TypeName = "text")]
    public string Content { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
using System.ComponentModel.DataAnnotations;

namespace PersonalBloggingPlatform.DTOs;
public sealed record CreatePostRequest(
    [param: Required, MaxLength(200)]
    string Title,
    [param: Required, MaxLength(10000)]
    string Content,
    [param: MaxLength(100)]
    string? Category,
    List<string>? Tags
);
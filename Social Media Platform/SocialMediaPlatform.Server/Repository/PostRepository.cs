using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Models;

namespace SocialMediaPlatform.Server.Repository;

public class PostRepository
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Post? GetPostById(int postId)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
        return post;
    }

    public IEnumerable<Post> GetAllPosts()
    {
        return _context.Posts;
    }

    public Post? CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
        return post;
    }
}
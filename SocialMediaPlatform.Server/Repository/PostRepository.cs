using SocialMediaPlatform.Server.Data;
using SocialMediaPlatform.Server.Dtos.Post;
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

    // public IEnumerable<Post> GetAllPosts()
    // {
    //     return _context.Posts;
    // }

    public List<PostWithUserDto> GetAllPostsWithUsers()
    {
        var postsWithUsers = (from post in _context.Posts
            join user in _context.Users
                on post.UserId equals user.Id
            orderby post.CreatedAt descending
            select new PostWithUserDto
            {
                Id = post.Id,
                ContentType = post.ContentType,
                CreatedAt = post.CreatedAt,
                Content = post.Content,
                MediaUrl = post.MediaUrl,
                UserId = post.UserId,
                Username = user.UserName
            }).ToList();

        return postsWithUsers;
    }
    public List<PostWithUserDto> GetPostsByUsername(string username)
    {
        var postsByUser = (from post in _context.Posts
                join user in _context.Users
                    on post.UserId equals user.Id
                where user.UserName == username 
                select new PostWithUserDto
                {
                    Id = post.Id,
                    ContentType = post.ContentType,
                    CreatedAt = post.CreatedAt,
                    Content = post.Content,
                    MediaUrl = post.MediaUrl,
                    UserId = post.UserId,
                    Username = user.UserName
                }).OrderByDescending(post => post.CreatedAt) 
            .ToList();

        return postsByUser;
    }


    public Post? CreatePost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
        return post;
    }

    public Post DeletePost(Post post)
    {
        _context.Posts.Remove(post);
        var comments = _context.Comments.Where(c => c.PostId == post.Id);
        foreach (var comment in comments)
        {
            _context.Comments.Remove(comment);
        }
        _context.SaveChanges();
        return post;
    }
}
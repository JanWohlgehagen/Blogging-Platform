using API.Core.Models;
using API.Core.MongoClient;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly BlogService _blogService;
    private readonly RedisClient _redisClient;
    
    public BlogController(BlogService blogService, RedisClient redisClient)
    {
        _blogService = blogService;
        _redisClient = redisClient;
        _redisClient.Connect();
    }

    [HttpPost]
    public IActionResult Post([FromBody] Blog blog)
    {
        var postResult = _blogService.saveBlog(blog);
        
        if (postResult)
        {
            return Ok("Post was successfully added to the database");
        }

        return BadRequest("Post was not added to the database");
    }
    
    [HttpGet]
    public IActionResult Get(string id)
    {

        var blog = _redisClient.GetCachedBlog(id);
        
        if (blog == null)
        {
            blog = _blogService.getBlog(id);
            if (blog == null)
            {
               return BadRequest("No blog found with the given id");
            }
            Console.WriteLine("Blog was not found in cache! Saving it...");
            _redisClient.CacheBlog(blog._id, blog);
        }
        Console.WriteLine("Blog was found in cache!");
        return Ok(blog);
        
    }
}
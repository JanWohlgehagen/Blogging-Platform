using API.Core.Models;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly BlogService _blogService;
    
    public BlogController(BlogService blogService)
    {
        _blogService = blogService;
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
}
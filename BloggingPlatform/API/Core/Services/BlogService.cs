using API.Core.Models;
using API.Core.Repository;

namespace API.Core.Services;

public class BlogService
{
    private readonly BlogRepository _blogRepository;
    
    public BlogService(BlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }
    
    public Blog getBlog(string id){
        return _blogRepository.GetById(id);
    }

    public bool saveBlog(Blog blog)
    {
        return _blogRepository.saveBlog(blog);
    }
}
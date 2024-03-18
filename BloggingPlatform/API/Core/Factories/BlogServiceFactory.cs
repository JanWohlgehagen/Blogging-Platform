using API.Core.MongoClient;
using API.Core.Repository;
using API.Core.Services;

namespace API.Core.Factories;

public static class BlogServiceFactory
{
    public static BlogService Create()
    {
        var client = new Client("mongodb://localhost:27017");
        var repository = new BlogRepository(client);
        return new BlogService(repository);
    }
}
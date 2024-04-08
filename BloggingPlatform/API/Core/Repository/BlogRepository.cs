using API.Core.Models;
using API.Core.MongoClient;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Core.Repository;

public class BlogRepository
{
    private readonly Client _client;
    private readonly string _databaseName;
    private readonly string _collectionName;
    
    public BlogRepository(Client client, string databaseName = "WannaBeReddit", string collectionName = "blogs")
    {
        _client = client;
        _databaseName = databaseName;
        _collectionName = collectionName;
    }

    public Blog GetById(string id)
    {
        var collection = _client.Collection<Blog>(_databaseName, _collectionName);
        
        return collection.Find(x => x._id == id).FirstOrDefault();
    }

    public bool saveBlog(Blog blog)
    {
        try
        {
            var collection = _client.Collection<Blog>(_databaseName, _collectionName);
            collection.InsertOne(blog);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
using System.Text.Json;
using API.Core.Models;
using StackExchange.Redis;

namespace API.Core.MongoClient;

public class RedisClient
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly string _password;
    private ConnectionMultiplexer _connection;
    
    public RedisClient(string hostname, int port, string password)
    {
        _hostname = hostname;
        _port = port;
        _password = password;
    }

    public void Connect()
    {
        _connection = ConnectionMultiplexer.Connect($"{_hostname}:{_port},password={_password}");
    }
    
    public void Disconnect()
    {
        _connection.Close();
    }
    
    public IDatabase getDatabase()
    {
        return _connection.GetDatabase();
    }
    
    public bool CacheBlog(string key, Blog blog)
    {
        try
        {
            var db = getDatabase();
            db.StringSet(key, JsonSerializer.Serialize(blog));
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool RemoveBlog(string key)
    {
        try
        {
            var db = getDatabase();
            db.KeyDelete(key);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public Blog GetCachedBlog(string key)
    {
        try
        {
            var db = getDatabase();
            var blog = db.StringGet(key);
            return JsonSerializer.Deserialize<Blog>(blog);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}
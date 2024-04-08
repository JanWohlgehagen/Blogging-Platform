using API.Core.MongoClient;

namespace API.Core.Factories;

public static class RedisClientFactory
{
    public static RedisClient Create()
    {
        return new RedisClient("localhost", 6379, "");
    }
}
using MongoDB.Bson;

namespace API.Core.Models;

public class Comment
{
    public string _id { get; set; }
    public string OwnerId { get; set; }
    public string Content { get; set; }
}
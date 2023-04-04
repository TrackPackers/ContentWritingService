using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace ContentWriterService.Models
{
    public class Content
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAT { get; set; }

        public Content()
        {
            this.CreatedAT = DateTime.UtcNow;
        }
    }
}

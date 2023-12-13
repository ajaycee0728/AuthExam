using System.Text.Json.Serialization;

namespace AuthExam.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CompletionFlag { get; set; }
        public bool isActive{ get; set; }
    }
}

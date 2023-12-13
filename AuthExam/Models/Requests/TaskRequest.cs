using System.ComponentModel.DataAnnotations;

namespace AuthExam.Models.Requests
{
    public enum CompletionFlag
    {
        Todo,
        InProgress,
        Completed
    }
    public class TaskRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string CompletionFlag { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}

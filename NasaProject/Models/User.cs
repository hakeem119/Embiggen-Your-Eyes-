using System.ComponentModel.DataAnnotations;

namespace NasaProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ICollection<Image> Images { get; set; }
        public ICollection<Annotation> Annotations { get; set; }
        public ICollection<ProcessingJob> ProcessingJobs { get; set; }
    }
}

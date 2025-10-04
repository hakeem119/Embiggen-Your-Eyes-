using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Models
{
    public class Annotation
    {
        [Key]
        public int AnnotationId { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string LabelName { get; set; }
        public string Geometry { get; set; } // GeoJSON or WKT
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Image Image { get; set; }
        public User User { get; set; }
    }
}

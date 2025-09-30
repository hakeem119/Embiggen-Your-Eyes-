using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Data
{
    public class Annotation
    {
        public int AnnotationId { get; set; }
        [ForeignKey("Image.ImageId")]
        public int ImageId { get; set; }    
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }         
        public string LabelName { get; set; }
        public string Geometry { get; set; }    // GeoJSON or WKT
        public DateTime CreatedAt { get; set; }
    }
}

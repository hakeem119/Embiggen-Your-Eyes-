using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Models
{
    public class ImageMetadata
    {
        [Key]
        public int MetadataId { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }

        public string Projection { get; set; }
        public string CoordinateSystem { get; set; }
        public float ResolutionX { get; set; }
        public float ResolutionY { get; set; }
        public int BandsCount { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // One-to-One
        public Image Image { get; set; }

    }
}

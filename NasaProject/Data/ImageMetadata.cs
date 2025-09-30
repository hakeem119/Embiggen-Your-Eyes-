using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Data
{
    public class ImageMetadata
    {
        public int MetadataId { get; set; }
        [ForeignKey("Image.ImageId")]
        public int ImageId { get; set; }     
        public string Projection { get; set; }
        public string CoordinateSystem { get; set; }
        public float ResolutionX { get; set; }
        public float ResolutionY { get; set; }
        public int BandsCount { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

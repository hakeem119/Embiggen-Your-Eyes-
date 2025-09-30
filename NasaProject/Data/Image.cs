using System.ComponentModel.DataAnnotations.Schema;

namespace NasaProject.Data
{
    public class Image
    {
        public int ImaheId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string status { get; set; }
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }
    }
}

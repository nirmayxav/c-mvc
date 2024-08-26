namespace SimpleImageGallery.Models
{
    public class ImageModel
    {
        public int Id { get; set; }          // Unique identifier for the image
        public string Title { get; set; }    // Title of the image
        public string FilePath { get; set; } // Path to the image file on the server
    }
}

using System.ComponentModel.DataAnnotations;

public class ImageModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Image Title")]
    public string Title { get; set; }

    [Required]
    public byte[] ImageData { get; set; }

    public string ContentType { get; set; }
}

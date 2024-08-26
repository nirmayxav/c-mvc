using Microsoft.AspNetCore.Mvc;
using SimpleImageGallery.Models;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SimpleImageGallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static List<ImageModel> _images = new List<ImageModel>();

        public GalleryController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // Show all uploaded images
        public IActionResult Index()
        {
            return View(_images);
        }

        // Upload an image
        [HttpPost]
        public IActionResult Upload(ImageModel model, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                // Save the uploaded image to the server
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }

                // Add image details to the list
                model.FilePath = $"/uploads/{uniqueFileName}";
                model.Id = _images.Count + 1;
                _images.Add(model);
            }

            return RedirectToAction("Index");
        }

        // Delete an image
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var image = _images.Find(img => img.Id == id);
            if (image != null)
            {
                // Delete the file from the server
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, image.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                // Remove image from the list
                _images.Remove(image);
            }

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks; // Import this namespace for asynchronous operations

namespace SolisDensCuraBETA.utilities
{
    public class ImageOperations
    {
        private readonly IWebHostEnvironment _env;

        public ImageOperations(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> ImageUpload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Create directory if it doesn't exist
            string uploadFolder = Path.Combine(_env.WebRootPath, "Images");
            Directory.CreateDirectory(uploadFolder);

            // Generate unique file name
            string uniqueFileName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(file.FileName);

            // Combine directory and file name to get full path
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            // Save file asynchronously
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
    }
}

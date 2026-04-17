using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemBLL.Services.AttachmentService
{
    internal class AttachementService : IAttachmentService
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly IWebHostEnvironment _webHost;
        public AttachementService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public string? Upload(string foldername, IFormFile file)
        {
            try {
                if (foldername is null || file is null || file.Length == 0)
                    return null;
                if (file.Length > maxFileSize)
                    return null;
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    return null;
                var folderPath = Path.Combine(_webHost.WebRootPath, "images", foldername);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);

                return fileName;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the file{foldername}: {ex.Message}");
                return null;
            }
        }
        public bool Delete(string filename, string foldername)
        {
            try
            {
                if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(foldername))
                    return false;

                var fullPath = Path.Combine(_webHost.WebRootPath, "images", foldername, filename);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file :{ex}");
                return false;
            }
        }
    }
}

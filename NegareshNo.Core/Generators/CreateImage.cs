using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NegareshNo.Core.Generators
{
    public static class CreateImage
    {
        public static string AddImageForCreateTime(IFormFile imageFile, string folderName, string defaultImage)
        {
            string imageName;
            if (imageFile != null)
            {
                imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                using (var stream = new FileStream(Directory.GetCurrentDirectory() + "\\wwwroot\\" + folderName + "\\" + imageName, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
            }
            else imageName = defaultImage;

            return imageName;
        }

        public static string AddImages(IFormFile ImageFile, string ImageName, string folderName, string defultImageName)
        {
            if (ImageName == null) ImageName = "NoImage.jpg";

            if (ImageFile != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + folderName, ImageName);

                if (ImageName != defultImageName)
                    if (File.Exists(imagePath)) File.Delete(imagePath);

                ImageName = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(ImageFile.FileName);

                using (var stream = new FileStream(Directory.GetCurrentDirectory() + "\\wwwroot\\" + folderName + "\\" + ImageName, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
            }

            return ImageName;
        }

        public static void DeleteImage(string ImageName, string folderName, string defultImageName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + folderName, ImageName);

            if (ImageName != defultImageName)
                if (File.Exists(imagePath)) File.Delete(imagePath);
        }
    }
}

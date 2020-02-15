using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Helper
{
    public class ImageHelper : IImageHelper
    {


        public async Task<string> UploadImageAsync(IFormFile ImageFile)
        {
            var path = string.Empty;
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot\\images\\Pets",
            file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }
            return $"~/images/Pets/{file}";
        }




    }



}

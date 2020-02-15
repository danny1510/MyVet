using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyVet.Web.Helper
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile ImageFile);
    }
}
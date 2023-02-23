using AdminPanel.Data;
using AdminPanel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.IO;
using WebApplication2.Models;

namespace AdminPanel.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = PoliciesNames.Administrator)]
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public ImagesController(IWebHostEnvironment env, ApplicationDbContext context)
        {
            _env = env;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(int productId, IFormFile file)
        {
            if (file != null)
            {
                string filetype = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1);
                Guid guid = Guid.NewGuid();
                string path = $"/images/{guid}.{filetype}";
                using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var image = new Image();
                image.ProductId = productId;
                image.Url = path;
                await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();

                return LocalRedirect($"/Products/EditProduct/{productId}");
            }

            return BadRequest("No file was uploaded.");
        }

        public async Task<IActionResult> DeleteImage(int imageId, int productId)
        {
            var image = await _context.Images.FirstAsync(x => x.Id == imageId);
            if(image is null)
            {
                return BadRequest("Image was not found");
            }
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();


            //  Path.Combine возвращает значение второго параметра, если в нем указан абсолютный путь (начинающийся на /)
            string fullPath = Path.Combine(_env.WebRootPath, image.Url);
            if(fullPath == image.Url) fullPath = _env.WebRootPath + image.Url;

            if (System.IO.File.Exists(fullPath))
            {
                try
                {
                    System.IO.File.Delete(fullPath);
                }
                catch (Exception e)
                {
                    //Debug.WriteLine(e.Message);
                }
            }

            return LocalRedirect($"/Products/EditProduct/{productId}");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DNSLab.Controllers
{
    [Route("Upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null;
            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/images/CKEditorImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/images/CKEditorImages/"}{fileName}";

            var success = new uploadsuccess
            {
                Uploaded = 1,
                FileName = fileName,
                Url = url
            };

            return new JsonResult(success);
        }
    }

    public class uploadsuccess
    {
        public int Uploaded { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}

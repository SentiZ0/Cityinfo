using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Cityinfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionsContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionsContentTypeProvider)
        {
            _fileExtensionsContentTypeProvider = fileExtensionsContentTypeProvider
             ?? throw new System.ArgumentException(nameof(fileExtensionsContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "getting-started-with-rest-slides.pdf";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if(!_fileExtensionsContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManagment;
using FileManagment.Entities;
using Journalist;
using MGSUBackend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MGSUCore.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFileManager _fileManager;

        public FilesController(IFileManager fileManager)
        {
            Require.NotNull(fileManager, nameof(fileManager));

            _fileManager = fileManager;
        }

        [HttpGet("{filename}")]
        [Route("file/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            return GetAnyFile(() => _fileManager.GetFile(fileName));
        }

        [HttpGet("{imageName}")]
        [Route("image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            return GetAnyFile(() => _fileManager.GetImage(imageName));
        }

        [HttpPost]
        [Route("file")]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                return Ok(await _fileManager.UploadFileAsync(Request.Body));
            }
            catch (NotSupportedException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("image")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var image = await _fileManager.UploadImageAsync(Request.Body);

                return Ok(new ImageModel
                {
                    Original = Path.GetFileName(image.Original.LocalPath),
                    Small = Path.GetFileName(image.Small.LocalPath),
                    Role = image.Role
                });
            }
            catch (NotSupportedException)
            {
                return BadRequest();
            }
            catch (InvalidDataException)
            {
                return NoContent();
            }
        }

        private IActionResult GetAnyFile(Func<Stream> getStream)
        {
            Stream stream;
            try
            {
                stream = getStream();
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }

            var response = new FileStreamResult(stream, "application/octet-stream");
            return response;
        }
    }
}

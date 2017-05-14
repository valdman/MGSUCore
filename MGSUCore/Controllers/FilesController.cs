using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileManagment;
using FileManagment.Entities;
using Journalist;
using MGSUBackend.Models;
using MGSUCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MGSUCore.Controllers
{
    [CustomExceptionFilter]
    public class FilesController : Controller
    {
        private readonly IFileManager _fileManager;

        public FilesController(IFileManager fileManager)
        {
            Require.NotNull(fileManager, nameof(fileManager));

            _fileManager = fileManager;
        }

        [HttpGet("{filename}")]
        [Route("file/{filename}")]
        public IActionResult GetFile([FromRoute]string fileName)
        {
            return GetAnyFile(() => _fileManager.GetFile(fileName));
        }

        [HttpGet("{imageName}")]
        [Route("image/{imageName}")]
        public IActionResult GetImage([FromRoute]string imageName)
        {
            return GetAnyFile(() => _fileManager.GetImage(imageName));
        }

        [HttpPost]
        [Route("file")]
        [Authorize("Admin")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
			if (file.Length <= 0)
			{
				return BadRequest();
			}

			var createdFileName = await _fileManager.UploadFileAsync(file);
            return Ok(createdFileName);
        }

        [HttpPost]
        [Route("image")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
			if (image.Length <= 0)
			{
                return BadRequest();
			}

            var createdImage = await _fileManager.UploadImageAsync(image);
            var imageModel = new ImageModel{
                  Original = createdImage.Original.Name,
                  Small = createdImage.Small.Name,
                  Role = createdImage.Role
                };
            return Ok(imageModel);
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

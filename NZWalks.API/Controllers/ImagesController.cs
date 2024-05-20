using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Reponsitories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //conver DTO to Domain model
                var imageDoMainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };


                //User reponsitory to upload image
                await imageRepository.Upload(imageDoMainModel);
                return Ok(imageDoMainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] {".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) 
            {
                ModelState.AddModelError("file", "Unsupported file extension");               
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size exceeds 10MB, Please upload a smaller size file.");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.FileManagement.Application.IService;
using Services.FileManagement.Application.Job;
using Services.FileManagement.Application.Models.Abstract;

namespace Services.FileManagement.API.Controllers
{
    [Route("api/FileManagement/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        private readonly UploadFileJob _uploadFileJob;
        private ResponseDto _response;

        public UploadController(IUploadService uploadService, UploadFileJob uploadFileJob)
        {
            _response = new ResponseDto();
            _uploadService = uploadService;
            _uploadFileJob = uploadFileJob;
        }
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile( IFormFile fileToUpload)
        {
            var file = await _uploadService.UploadFileAsync(fileToUpload);
            await _uploadFileJob.EnqueueAsync();
            return Ok(_response.Result = file);
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles( List<IFormFile> filesToUpload)
        {
            var files = await _uploadService.UploadFilesAsync(filesToUpload);
            await _uploadFileJob.EnqueueAsync();
            return Ok(_response.Result = files);
        }
    }
}

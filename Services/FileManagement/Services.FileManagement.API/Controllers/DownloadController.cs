using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.FileManagement.Application.IService;
using Services.FileManagement.Application.Job;
using Services.FileManagement.Application.Models.Abstract;
using Services.FileManagement.Application.Models.Dto.MediaFile;

namespace Services.FileManagement.API.Controllers
{
    [Route("api/FileManagement/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {

        private readonly IDownloadService _downloadService;
        private ResponseDto _response;

        public DownloadController(IDownloadService downloadService)
        {
            _response = new ResponseDto();
            _downloadService = downloadService;
        }

        [HttpGet("DownloadFile/{fileId}")]
        public async Task<FileStreamResult> DownloadFile(string fileId)
        {

            return await _downloadService.Download(fileId);

        }

        [HttpGet("DownloadFileAsBase64/{fileId}")]
        public async Task<IActionResult> DownloadFileAsBase64(string fileId)
        {

            var file = await _downloadService.DownloadAsBase64(fileId);
            _response.Result = new Filebase64Dto { FileBytes = file };
            return Ok(_response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Services.FileManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.IService
{
    public interface IDownloadService
    {
        Task<FileStreamResult> Download(string Id);
        Task<string> DownloadAsBase64(string Id);
    }
}

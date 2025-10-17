using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.FileManagement.Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Job
{
    public class ScheduledUploadFileJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public ScheduledUploadFileJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var uploadService = scope.ServiceProvider.GetRequiredService<IUploadService>();
                await uploadService.SaveFiles();

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}

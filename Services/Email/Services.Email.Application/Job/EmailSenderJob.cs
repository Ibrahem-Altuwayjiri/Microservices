using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Email.Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Services.Email.Application.Job
{
    public class EmailSenderJob : BackgroundService
    {
        private readonly Channel<bool> _channel = Channel.CreateUnbounded<bool>();
        private readonly IServiceProvider _serviceProvider;

        public EmailSenderJob( IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // Trigger this method from controller
        public async Task EnqueueAsync()
        {
            await _channel.Writer.WriteAsync(true);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var _ in _channel.Reader.ReadAllAsync(stoppingToken))
            {
                using var scope = _serviceProvider.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                await emailService.Send();
            }
        }
    }
}

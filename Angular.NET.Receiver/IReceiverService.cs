using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.NET.Receiver
{
    public interface IReceiverService : IHostedService
    {
        /// <summary>
        /// Start TCP listener and start receiving data bytes
        /// </summary>
        /// <returns></returns>
        public Task StartReceivingAsync(CancellationToken stoppingToken);

        /// <summary>
        /// Stop TCP listener and stop receiving data bytes
        /// </summary>
        /// <returns></returns>
        public Task StopReceivingAsync();
    }
}

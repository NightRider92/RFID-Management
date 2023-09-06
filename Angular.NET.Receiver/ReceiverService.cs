using Angular.NET.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Angular.NET.Receiver
{
    public class ReceiverService : BackgroundService, IReceiverService
    {
        private readonly ILogger<ReceiverService> logger;
        private readonly IDatabaseService databaseService;
        private TcpListener listener;
        private IPEndPoint endpoint;

        public ReceiverService(ILogger<ReceiverService> _logger, IDatabaseService _databaseService)
        {
            this.logger = _logger;
            this.logger.LogInformation("Initializing receiver service");
            this.databaseService = _databaseService;
            this.endpoint = new IPEndPoint(IPAddress.Any, 25755);
            this.listener = new TcpListener(this.endpoint);
        }

        /// <summary>
        /// Handle connected TCP client
        /// </summary>
        /// <param name="client"></param>
        private async Task HandleAsync(TcpClient client)
        {
            // Get the client's network stream for reading and writing data.
            NetworkStream stream = client.GetStream();

            try
            {
                // Buffer for reading data.
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Convert the received data to a string.
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                    // Log the received data.
                    this.logger.LogInformation($"Received: {data}");
                    bool hasAccess = await this.databaseService.UserHasAccessAsync(data);

                    if (!hasAccess) this.logger.LogInformation($"RFID value: {data} does not have access to the system");
                    else this.logger.LogInformation($"RFID value: {data} has access to the system");

                    // Echo the data back to the client.
                    byte[] responseData = Encoding.UTF8.GetBytes($"Server: {data}");
                    stream.Write(responseData, 0, responseData.Length);
                }
            }
            catch (IOException)
            {
                // Handle client disconnection gracefully.
                Console.WriteLine($"Client disconnected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");
            }
            finally
            {
                // Clean up resources when the client disconnects.
                stream.Close();
                client.Close();
            }
        }

        /// <inheritdoc />
        public async Task StartReceivingAsync(CancellationToken stoppingToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(this.listener);
                this.listener.Start();
                this.logger.LogInformation($"Server listening on port {this.endpoint.Port}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    TcpClient client = await this.listener.AcceptTcpClientAsync();
                    this.logger.LogInformation($"Client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");
                    new Thread(async () =>
                    {
                        await this.HandleAsync(client);
                    }).Start();
                }
            }
            catch (Exception ex) { this.logger.LogError(ex.ToString()); }
            finally { await this.StopReceivingAsync(); }
        }

        /// <inheritdoc />
        public Task StopReceivingAsync()
        {
            this.listener.Stop();
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try { await this.StartReceivingAsync(stoppingToken); }
            catch (Exception ex) { this.logger.LogError(ex.ToString()); }
        }
    }
}
using System.Diagnostics;

namespace WebStorePrototype.Server.Services.Handlers
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _stopwatch = new();
        public LoggingHandler(ILogger logger)
        {
            _logger = logger;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Sending request To KeepingCRMAPI");
                _stopwatch.Start();
                var response = base.SendAsync(request, cancellationToken);
                _stopwatch.Stop();
                _logger.LogInformation($"Request completed in {_stopwatch.ElapsedMilliseconds} ms.");
                _stopwatch.Reset();

                return response;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

    }
}

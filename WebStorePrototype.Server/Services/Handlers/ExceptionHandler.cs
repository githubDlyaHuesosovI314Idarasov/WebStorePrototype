using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens.Experimental;
using Refit;
using System.Net;

namespace WebStorePrototype.Server.Services.Handlers
{
    public class ExceptionHandler : DelegatingHandler
    {
        private readonly ILogger _logger;
        public ExceptionHandler(ILogger logger) 
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return new HttpResponseMessage(ex.StatusCode);
            }
        }
       
    } 
}

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MovieTime.Services
{
    public class TmdbApiKeyHandler : HttpClientHandler
    {
        private const string ApiKeyQueryStringName = "api_key";
        private readonly string apiKey;

        public TmdbApiKeyHandler(string apiKey)
        {
            this.apiKey = apiKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = AppendApiKeyToUri(request.RequestUri);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private Uri AppendApiKeyToUri(Uri uri)
        {
            var query = HttpUtility.ParseQueryString(uri.Query);
            query.Set(ApiKeyQueryStringName, apiKey);
            return new UriBuilder(uri) { Query = query.ToString() }.Uri;
        }
    }
}

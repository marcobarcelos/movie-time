using System;
using System.Net.Http;
using MovieTime.Consts;
using MovieTime.Services;
using Refit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TmdbService))]
namespace MovieTime.Services
{
    public class TmdbService : ITmdbService
    {
        private ITmdbApi api;

        public ITmdbApi GetInstance()
        {
            if (api == null)
            {
                var handler = new TmdbApiKeyHandler(AppSettings.TmdbApiKey);
                var client = new HttpClient(handler) { BaseAddress = new Uri(AppSettings.TmdbApiUrl) };
                api = RestService.For<ITmdbApi>(client);
            }

            return api;
        }
    }
}

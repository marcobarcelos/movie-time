using System;
using System.Threading.Tasks;
using MovieTime.Models;
using MovieTime.Services;
using MovieTime.Utils;
using Xamarin.Forms;

namespace MovieTime.PageModels
{
    public class MovieDetailPageModel : BasePageModel
    {
        private Movie movie;

        public MovieDetail Detail { get; set; }
        public string Genres { get; set; }

        public override async void Init(object initData)
        {
            base.Init(initData);
            movie = initData as Movie;
            Title = movie.Title;
            IsLoading = true;
            await FetchMovieDetail();
        }

        public async Task FetchMovieDetail()
        {
            try
            {
                var service = DependencyService.Get<ITmdbService>().GetInstance();
                Detail = await service.GetMovieDetail(movie.Id);
                Genres = Detail.Genres.GetGenreDescription();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                await CoreMethods?.DisplayAlert("Error", "An error occurred while loading movie information.", "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}

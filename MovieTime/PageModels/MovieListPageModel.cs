using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MovieTime.Models;
using MovieTime.Services;
using MovieTime.Utils;
using Xamarin.Forms;

namespace MovieTime.PageModels
{
    public class MovieListPageModel : BasePageModel
    {
        private const int LoadThresholdItemCount = 5;

        private int currentPage;
        private int totalPages;
        private Dictionary<int, Genre> genres;

        public ICommand SelectMovieCommand { get; set; }
        public ICommand FetchMoviesCommand { get; set; }
        public ICommand FetchMoviesNextPageCommand { get; set; }

        public bool IsRefreshing { get; set; }
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        public MovieListPageModel()
        {
            SelectMovieCommand = new Command<Movie>(SelectMovie);
            FetchMoviesCommand = new Command(async () => await FetchMovies());
            FetchMoviesNextPageCommand = new Command<Movie>(async (movie) => await FetchMoviesNextPage(movie));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);
            IsLoading = true;
            await FetchMovies();
        }

        void SelectMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        void ClearMovies()
        {
            Movies?.Clear();
        }

        async Task FetchGenres()
        {
            try
            {
                var service = DependencyService.Get<ITmdbService>().GetInstance();
                var genreList = await service.GetGenres();
                genres = genreList.Genres.ToDictionary(genre => genre.Id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                await CoreMethods?.DisplayAlert("Error", "An error occurred while loading movie genres.", "Ok");
            }
        }

        async Task<string> GetMovieGenreDescription(List<int> genreIds)
        {
            if (genres == null)
                await FetchGenres();

            return genreIds.Select(id =>
            {
                genres.TryGetValue(id, out Genre val);
                return val;
            }).OfType<Genre>().GetGenreDescription();
        }

        async Task FetchMovies(int page)
        {
            try
            {
                var service = DependencyService.Get<ITmdbService>().GetInstance();
                var movies = await service.GetUpcomingMovies(page);

                foreach (var movie in movies.Results)
                {
                    movie.GenreDescription = await GetMovieGenreDescription(movie.GenreIds);
                    Movies.Add(movie);
                }

                currentPage = page;
                totalPages = movies.TotalPages;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                await CoreMethods?.DisplayAlert("Error", "An error occurred while loading movies.", "Ok");
            }
            finally
            {
                IsRefreshing = IsLoading = false;
            }
        }

        async Task FetchMovies()
        {
            ClearMovies();
            await FetchMovies(1);
        }

        async Task FetchMoviesNextPage(Movie movie)
        {
            var currentIndex = Movies.IndexOf(movie);
            var lastIndex = Movies.Count - 1;
            var thresholdIndex = lastIndex - LoadThresholdItemCount;
            var hasPagesToScroll = currentPage < totalPages;

            if (currentIndex == thresholdIndex && hasPagesToScroll)
                await FetchMovies(currentPage + 1);
        }
    }
}

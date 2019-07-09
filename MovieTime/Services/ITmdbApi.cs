using System.Threading.Tasks;
using MovieTime.Models;
using Refit;

namespace MovieTime.Services
{
    public interface ITmdbApi
    {
        [Get("/movie/{id}")]
        Task<MovieDetail> GetMovieDetail(int id);

        [Get("/movie/upcoming")]
        Task<PagedResult<Movie>> GetUpcomingMovies(int page = 1);

        [Get("/genre/movie/list")]
        Task<GenreList> GetGenres();
    }
}

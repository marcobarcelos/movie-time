using System.Collections.Generic;
using System.Linq;
using MovieTime.Models;

namespace MovieTime.Utils
{
    public static class GenreExtensions
    {
        public static string GetGenreDescription(this IEnumerable<Genre> genres)
        {
            return string.Join(", ", genres?.Select(genre => genre.Name) ?? Enumerable.Empty<string>());
        }

        public static string GetGenreDescription(this GenreList genreList)
        {
            return genreList.Genres.GetGenreDescription();
        }
    }
}

namespace Models.Entities
{
    public enum Genre
    {
        Fiction,
        NonFiction,
        Mystery,
        Thriller,
        Romance,
        Fantasy,
        ScienceFiction,
        Horror,
        Biography,
        Autobiography,
        History,
        Philosophy,
        Poetry,
        Drama,
        Comedy,
        Adventure,
        Crime,
        Suspense,
        Western,
        Contemporary,
        Classic,
        YoungAdult,
        Children,
        SelfHelp,
        Business,
        Technology,
        Science,
        Health,
        Travel,
        Cooking,
        Art,
        Music,
        Sports,
        Religion,
        Spirituality,
        Politics,
        Economics,
        Psychology,
        Education,
        Reference,
        Other
    }

    public static class GenreExtensions
    {
        public static string ToDisplayString(this Genre genre)
        {
            return genre switch
            {
                Genre.ScienceFiction => "Science Fiction",
                Genre.YoungAdult => "Young Adult",
                Genre.NonFiction => "Non-Fiction",
                Genre.SelfHelp => "Self Help",
                _ => genre.ToString()
            };
        }

        public static Genre FromString(string genreString)
        {
            if (string.IsNullOrWhiteSpace(genreString))
                return Genre.Other;

            return genreString.ToLowerInvariant().Replace(" ", "").Replace("-", "") switch
            {
                "sciencefiction" or "scifi" => Genre.ScienceFiction,
                "youngadult" or "ya" => Genre.YoungAdult,
                "nonfiction" => Genre.NonFiction,
                "selfhelp" => Genre.SelfHelp,
                _ => Enum.TryParse<Genre>(genreString, true, out var result) ? result : Genre.Other
            };
        }
    }
}

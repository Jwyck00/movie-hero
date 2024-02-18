namespace Infrastructure.Persistence.Common;

public static class DbConfiguration
{
    public static string GetConnectionString()
    {
        var solutionDirectory = TryGetSolutionDirectory();
        var dbPath = Path.Combine(solutionDirectory, "MovieHero.db");
        return $"Data Source={dbPath}";
    }

    private static string TryGetSolutionDirectory()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }

        if (directory == null)
            throw new FileNotFoundException(
                "No solution file found in the project! (searching for \"*.sln\""
            );

        return directory.FullName;
    }
}

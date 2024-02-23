namespace Application.Movies.Common;

public record EditMoviesRequest(string Name, IList<Guid> ActorIds);

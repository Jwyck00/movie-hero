using Application.Movies.Common;
using FluentValidation;
using MediatR;

namespace Application.Movies.Commands.EditMovie;

public class EditMovieCommand : IRequest<MovieResponse>
{
    public Guid MovieId { get; set; }

    public string Name { get; set; }
    public IList<Guid> ActorIds { get; set; } = new List<Guid>();
}

public class CreateMovieCommandValidator : AbstractValidator<EditMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}

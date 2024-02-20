using Application.Movies.Common;
using FluentValidation;
using MediatR;

namespace Application.Movies.Commands.EditMovie;

public class EditMovieCommand : IRequest<MovieResponse>
{
    public Guid MovieId { get; set; }

    public string Name { get; set; }
}

public class CreateMovieCommandValidator : AbstractValidator<EditMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}

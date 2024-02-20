using Application.Movies.Common;
using FluentValidation;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public record CreateMovieCommand(string Name) : IRequest<MovieResponse>;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(30);
    }
}

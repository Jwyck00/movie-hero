using Application.Movies.Common;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieCommand : IRequest<MovieResponse>, IMapFrom<CreateMovieCommand>
{
    public string Name { get; set; } = null!;
}

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}

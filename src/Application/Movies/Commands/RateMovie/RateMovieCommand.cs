using FluentValidation;
using MediatR;

namespace Application.Movies.Commands.RateMovie;

public class RateMovieCommand : IRequest
{
    public Guid MovieId { get; set; }
    public int Rate { get; set; }
}

public class RateMovieCommandValidator : AbstractValidator<RateMovieCommand>
{
    public RateMovieCommandValidator()
    {
        RuleFor(x => x.Rate).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
    }
}

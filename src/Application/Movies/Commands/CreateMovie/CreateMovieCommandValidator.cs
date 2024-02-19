using FluentValidation;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(30);
    }
}

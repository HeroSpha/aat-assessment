using FluentValidation;

namespace Assessment3.Server.Application.Events.Commands.Create;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();
        RuleFor(x => x.Description)
            .NotEmpty();
        RuleFor(x => x.Venue)
            .NotEmpty();
        RuleFor(x => x.Image)
            .NotEmpty();
        RuleFor(x => x.Date)
            .NotNull();
        RuleFor(x => x.Seats)
            .GreaterThan(0);
    }
}
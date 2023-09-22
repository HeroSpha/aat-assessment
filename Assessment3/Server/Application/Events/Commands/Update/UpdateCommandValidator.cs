using FluentValidation;

namespace Assessment3.Server.Application.Events.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
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
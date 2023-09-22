using FluentValidation;

namespace Assessment3.Server.Application.Events.Commands.Delete;

public class EventDeleteCommandValidator : AbstractValidator<EventDeleteCommand>
{
    public EventDeleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}
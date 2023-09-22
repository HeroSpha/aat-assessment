﻿using Assessment3.Server.Application.Authentication.Common;
using Assessment3.Server.Application.Events.Commom;
using MediatR;
using ErrorOr;

namespace Assessment3.Server.Application.Events.Commands.Update;

public record UpdateCommand(
    Guid Id,
    string Title,
    string Description,
    string Image,
    string Venue,
    DateTime Date,
    int Seats) : IRequest<ErrorOr<EventResult>>;
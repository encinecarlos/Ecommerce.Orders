﻿namespace Order.Email.Api.Domain.Interfaces;

public interface IMessageHandlerService
{
    Task SendMessage(Entities.Email messageEmail);
}
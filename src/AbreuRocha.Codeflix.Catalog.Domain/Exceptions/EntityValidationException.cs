﻿namespace AbreuRocha.Codeflix.Catalog.Domain.Exceptions;
public class EntityValidationException : Exception
{
    public EntityValidationException(string? message) : base(message)
    {
    }
}

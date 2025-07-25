﻿using AbreuRocha.Codeflix.Catalog.Domain.Exceptions;
using AbreuRocha.Codeflix.Catalog.Domain.SeedWork;
using AbreuRocha.Codeflix.Catalog.Domain.Validation;

namespace AbreuRocha.Codeflix.Catalog.Domain.Entity;
public class Category : AggregateRoot
{
    public Category(string name, string description, bool isActive = true)
        : base()
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;

        Validate();
    }
    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name,3,nameof(Name));
        DomainValidation.MaxLength(Name,255,nameof(Name));

        DomainValidation.NotNull(Description, nameof(Description));
        DomainValidation.MaxLength(Description, 10_000, nameof(Description));
    }
}

using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.Common;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbreuRocha.Codeflix.Catalog.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) 
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CategoryModelOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CategoryModelOutput), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CategoryModelOutput), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryInput input, 
        CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create), 
            new { output.Id }, output);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryModelOutput), StatusCodes.Status200OK)]

    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetCategoryInput(id), cancellationToken);
        return Ok(output);
    }
}

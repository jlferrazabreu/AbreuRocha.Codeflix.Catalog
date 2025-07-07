using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
public class DeleteCategoryInput : IRequest
{
    public DeleteCategoryInput(Guid id)
        => Id = id;
    public Guid Id { get; }

}

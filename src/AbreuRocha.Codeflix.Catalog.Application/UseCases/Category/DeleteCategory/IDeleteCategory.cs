using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;
public interface IDeleteCategory 
    : IRequestHandler<DeleteCategoryInput>
{}

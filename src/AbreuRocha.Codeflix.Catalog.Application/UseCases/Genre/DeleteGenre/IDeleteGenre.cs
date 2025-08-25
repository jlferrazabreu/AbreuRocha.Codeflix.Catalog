using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.DeleteGenre;
public interface IDeleteGenre
    : IRequestHandler<DeleteGenreInput>
{ }

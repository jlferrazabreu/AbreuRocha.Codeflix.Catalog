using AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.UpdateGenre;
public interface IUpdateGenre
    : IRequestHandler<UpdateGenreInput, GenreModelOutput>
{ }

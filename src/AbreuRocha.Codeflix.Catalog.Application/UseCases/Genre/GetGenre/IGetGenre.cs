using AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.GetGenre;
public interface IGetGenre
    : IRequestHandler<GetGenreInput, GenreModelOutput>
{ }

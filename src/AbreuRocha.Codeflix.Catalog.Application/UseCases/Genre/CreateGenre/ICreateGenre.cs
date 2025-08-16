using AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace AbreuRocha.Codeflix.Catalog.Application.UseCases.Genre.CreateGenre;
public interface ICreateGenre
    : IRequestHandler<CreateGenreInput, GenreModelOutput>
{ }

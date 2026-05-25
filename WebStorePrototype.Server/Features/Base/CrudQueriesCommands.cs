using DAL;
using MediatR;

namespace WebStorePrototype.Server.Features.Base
{
    public record GetByIdQuery<T>(Guid Id) : IRequest<T?> where T : Entity;
    public record GetAllQuery<T>() : IRequest<IEnumerable<T>> where T : Entity;
    public record CreateCommand<T>(T Entity) : IRequest<T> where T : Entity;
    public record UpdateCommand<T>(T Entity) : IRequest<T> where T : Entity;
    public record DeleteCommand<T>(Guid Id) : IRequest where T : Entity;
}

using Ardalis.Specification;
using Core.Entities;

namespace Core.Interfaces;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : BaseEntity
{
}
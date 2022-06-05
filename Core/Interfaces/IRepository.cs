using Ardalis.Specification;
using Core.Entities;

namespace Core.Interfaces;
public interface IRepository<T> : IRepositoryBase<T> where T : BaseEntity
{
}
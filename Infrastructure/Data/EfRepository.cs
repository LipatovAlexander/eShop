using Ardalis.Specification.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : BaseEntity
{
    public EfRepository (ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
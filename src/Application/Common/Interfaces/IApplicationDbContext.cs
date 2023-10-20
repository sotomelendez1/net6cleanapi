using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Movie> Movies {  get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

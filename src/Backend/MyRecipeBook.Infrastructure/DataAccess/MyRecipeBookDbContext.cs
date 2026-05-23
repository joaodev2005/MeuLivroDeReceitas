using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

[assembly: InternalsVisibleTo("WebApi.Tests")]
namespace MyRecipeBook.Infrastructure.DataAccess;

internal class MyRecipeBookDbContext : DbContext
{
    public MyRecipeBookDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}

    public DbSet<User> Users { get; set; }
}

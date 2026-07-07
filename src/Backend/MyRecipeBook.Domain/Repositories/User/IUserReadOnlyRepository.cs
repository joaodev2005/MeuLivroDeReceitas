namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithId(Guid userId);
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Domain.Entities.User?> GetByEmail(string email);
}

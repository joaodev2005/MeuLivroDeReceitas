namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserUpdateOnlyRepository
{
    void UpdateProfile(Entities.User user);
    Task Updatepassword(Guid userId, string passwordHash);
}

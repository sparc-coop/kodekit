namespace Kodekit;

public class UpdateUser(IRepository<User> users) : PublicFeature<User, bool>
{
    public IRepository<User> Users { get; } = users;

    public override async Task<bool> ExecuteAsync(User user)
    {
        try
        {
            await Users.UpdateAsync(user);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

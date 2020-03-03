using TaskManager.Library.Models;

namespace TaskManager.Library.DataProviders
{
    public interface IUserInfoProvider
    {
        User GetUser();
    }
}
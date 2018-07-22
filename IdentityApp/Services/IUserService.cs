using System.Threading.Tasks;

namespace IdentityApp.Services
{
    public interface IUserService
    {
        Task<bool> AddCredetials(string username, string password, out User user);
        Task<bool> AddUser(string username, string password);
    }

    public class User
    {

        public User(string username)
        {
            UserName = username;
        }

        public string UserName { get; }
    }
}

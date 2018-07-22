using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityApp.Services
{
    public class UserService : IUserService
    {

        private IDictionary<string, (string PasswordHash, User User)> _users =
            new Dictionary<string, (string PasswordHash, User User)>();

        public UserService(IDictionary<string, string> users)
        {
            foreach (var user in users)
            {
                _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));
            }
        }
        public Task<bool> AddCredetials(string username, string password, out User user)
        {
            user = null;
            var key = username.ToLower();

            if (_users.ContainsKey(key))
            {
                var hash = _users[key].PasswordHash;

                if (BCrypt.Net.BCrypt.Verify(password, hash))
                {
                    user = _users[key].User;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> AddUser(string username, string password)
        {
            if (_users.ContainsKey(username.ToLower()))
            {
                return Task.FromResult(false);
            }
            _users.Add(username.ToLower(), (BCrypt.Net.BCrypt.HashPassword(password), new User(username)));

            return Task.FromResult(true);
        }
    }
}

using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepsoitory : IUserRepository
    {
        private List<User> users = new List<User>()
        {
            new User() { FirstName = "Read Only", LastName = "User", EmailAddress = "readonly@user.com",
                Id = Guid.NewGuid(), UserName = "readonly@user.com", Password = "readonly",Roles = new List<string>{"reader"}
            },
            new User() { FirstName = "Read Write", LastName = "User", EmailAddress = "readwrite@user.com",
                Id = Guid.NewGuid(), UserName = "readwrite@user.com", Password = "readwrite",Roles = new List<string>{"reader","writer"}
            }
        };

        public Task<bool> AuthenticateAsync(string userName, string password)
        {
            return Task.FromResult(false);
        }
    }
}

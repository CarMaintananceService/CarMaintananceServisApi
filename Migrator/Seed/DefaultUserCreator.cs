using EntityFrameworkCore.Data;
using Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Seed
{
    public class DefaultUsersCreator
    {
        public static List<User> InitialUsers => GetInitialUsers();

        private readonly ApplicationDbContext _context;

        private static List<User> GetInitialUsers()
        {
            return new List<User>
            {
                new User(){ UserName = "admin" }
            };
        }

        public DefaultUsersCreator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync()
        {
            await CreateUsersAsync();
        }

        private async Task CreateUsersAsync()
        {
            foreach (var user in InitialUsers)
            {
               // await AddLanguageIfNotExistsAsync(user);
            }
        }

        //private async Task AddLanguageIfNotExistsAsync(User user)
        //{
        //    if (_context.Users.Any(l => l.UserName.ToLower() == user.UserName.ToLower()))
        //    {
        //        return;
        //    }

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //}
    }
}

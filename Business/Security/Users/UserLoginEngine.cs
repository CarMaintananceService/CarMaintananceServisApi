using Core.Security;
using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security.Users
{
    public class UserLoginEngine
    {
        ApplicationDbContext _dbContext;

        public UserLoginEngine(
            ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }


        public async Task<User> GetById(int id)
        {
            User user = await _dbContext.Users.AsNoTracking().IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }



        public async Task<User> GetByUserName(string userName)
        {
            User user = await _dbContext.Users
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);
            return user;
        }

        public async Task UpdateTokenInfo(int id, string refreshToken, DateTime refreshTokenExpireIn, string userRightsView)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireIn = refreshTokenExpireIn;
            user.UserRightsView = userRightsView;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RevokeRefreshToken(int id)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id);
            user.RefreshToken = null;
            user.RefreshTokenExpireIn = null;
            user.UserRightsView = null;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }



    }
}

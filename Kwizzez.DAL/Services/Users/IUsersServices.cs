using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Users
{
    public interface IUsersService
    {
        List<UserDto> GetAllUsers(QueryFilter<ApplicationUser> queryFilter);
        UserDto GetUserById(string id, string includeProperties = "");
        UserDto GetUserByEmail(string email, string includeProperties = "");
        UserDto GetUserByUserName(string userName, string includeProperties = "");
    }
}
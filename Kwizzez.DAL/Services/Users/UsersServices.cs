using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kwizzez.DAL.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public List<UserDto> GetAllUsers(QueryFilter<ApplicationUser> queryFilter)
        {
            var users = _userManager.Users;

            foreach (var property in queryFilter.IncludeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            if (queryFilter.Filter != null)
                users = users.Where(queryFilter.Filter);

            if (queryFilter.OrderExpression != null)
                users = queryFilter.OrderExpression(users);

            if (queryFilter.Skip > 0)
                users = users.Skip(queryFilter.Skip);

            if (queryFilter.Take > 0)
                users = users.Take(queryFilter.Take);

            List<UserDto> usersDtos = new();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = (List<string>)_userManager.GetRolesAsync(user).Result;
                usersDtos.Add(userDto);
            }
            return usersDtos;
        }

        public UserDto GetUserByEmail(string email, string includeProperties = "")
        {
            var users = _userManager.Users;
            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            var user = users.FirstOrDefault(u => u.Email == email);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = (List<string>)_userManager.GetRolesAsync(user).Result;
            return userDto;
        }

        public UserDto GetUserById(string id, string includeProperties = "")
        {
            var users = _userManager.Users;
            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            var user = users.FirstOrDefault(u => u.Id == id);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = (List<string>)_userManager.GetRolesAsync(user).Result;
            return userDto;
        }

        public UserDto GetUserByUserName(string userName, string includeProperties = "")
        {
            var users = _userManager.Users;
            foreach (var property in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                users = users.Include(property);

            var user = users.FirstOrDefault(u => u.UserName == userName);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = (List<string>)_userManager.GetRolesAsync(user).Result;
            return userDto;
        }
    }
}
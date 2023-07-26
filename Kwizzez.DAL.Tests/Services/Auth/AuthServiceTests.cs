using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Auth;
using Kwizzez.DAL.Services.Auth;
using Kwizzez.Domain.Constants;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Kwizzez.DAL.Tests.Services.Auth
{
    // public class AuthServiceTests
    // {
    //     private readonly AuthService _authService;
    //     private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    //     private readonly Mock<JwtSecurityTokenHandler> _jwtSecurityTokenHandlerMock;
    //     private readonly ApplicationUser _student = new()
    //     {
    //         Id = new Guid().ToString(),
    //         UserName = "TestStudent",
    //         Email = "testStudent@example.com",
    //         FirstName = "Joe",
    //         LastName = "Michael",
    //         DateOfBirth = new DateTime(1999, 1, 1)
    //     };
    //     private readonly ApplicationUser _duplicatedStudent = new()
    //     {
    //         Id = new Guid().ToString(),
    //         UserName = "TestDuplicatedStudent",
    //         Email = "testDuplicatedStudent@example.com",
    //         FirstName = "Joe",
    //         LastName = "Michael",
    //         DateOfBirth = new DateTime(1999, 1, 1)
    //     };
    //     private readonly ApplicationUser _teacher = new()
    //     {
    //         Id = new Guid().ToString(),
    //         UserName = "TestTeacher",
    //         Email = "testTeacher@example.com",
    //         FirstName = "Joe",
    //         LastName = "Michael",
    //         DateOfBirth = new DateTime(1999, 1, 1)
    //     };
    //     private readonly string _password = "Hello%world1";

    //     public AuthServiceTests()
    //     {
    //         _userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
    //         _jwtSecurityTokenHandlerMock = new Mock<JwtSecurityTokenHandler>();

    //         // Setup mocks
    //         _userManagerMock
    //             .Setup(m => m.FindByEmailAsync(_student.Email).Result)
    //             .Returns(_student);

    //         _userManagerMock
    //             .Setup(m => m.CreateAsync(_student).Result)
    //             .Returns(new IdentityResult());

    //         _userManagerMock
    //             .Setup(m => m.AddPasswordAsync(_student, _password).Result)
    //             .Returns(new IdentityResult());

    //         _userManagerMock
    //             .Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), Roles.Teacher).Result)
    //             .Returns(new IdentityResult());

    //         _userManagerMock
    //             .Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), Roles.Student).Result)
    //             .Returns(new IdentityResult());

    //         _userManagerMock
    //             .Setup(m => m.GetRolesAsync(It.Is<ApplicationUser>(u => u.Email == _student.Email)).Result)
    //             .Returns(new List<string>() { Roles.Student });

    //         _userManagerMock
    //                 .Setup(m => m.GetRolesAsync(It.Is<ApplicationUser>(u => u.Email == _teacher.Email)).Result)
    //                 .Returns(new List<string>() { Roles.Teacher });

    //         _userManagerMock
    //             .Setup(m => m.UpdateAsync(It.IsAny<ApplicationUser>()).Result)
    //             .Returns(new IdentityResult());


    //         _authService = new AuthService(_userManagerMock.Object);
    //     }

    //     [Fact]
    //     public async Task AuthService_RegisterValidStudent_ShouldReturnSucceedIdentityResult()
    //     {
    //         // Arrange
    //         RegisterDto registerDto = new()
    //         {
    //             Email = _student.Email,
    //             Password = _password,
    //             UserName = _student.UserName,
    //             FirstName = _student.FirstName,
    //             LastName = _student.LastName,
    //             DateOfBirth = _student.DateOfBirth,
    //             IsTeacher = false
    //         };

    //         // Act
    //         var authModel = await _authService.RegisterUserAsync(registerDto);

    //         // Assert
    //         Assert.True(authModel.IsSucceed);
    //         Assert.Null(authModel.Errors);
    //         Assert.Equal(_student.Email, authModel.Email);
    //         Assert.Equal(_student.UserName, authModel.UserName);
    //         Assert.Equal(_student.FirstName, authModel.FirstName);
    //         Assert.NotNull(authModel.Roles);
    //         Assert.Single(authModel.Roles);
    //         Assert.Contains(Roles.Student, authModel.Roles);

    //         _userManagerMock
    //             .Verify(mock => mock.CreateAsync(It.Is<ApplicationUser>(u => u.Email == _student.Email)).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(mock => mock.AddPasswordAsync(It.Is<ApplicationUser>(u => u.Email == _student.Email), _password).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(mock => mock.AddToRoleAsync(It.Is<ApplicationUser>(u => u.Email == _student.Email), Roles.Student).Result, Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_RegisterValidTeacher_ShouldReturnSucceedAuthDto()
    //     {
    //         // Arrange
    //         RegisterDto registerDto = new()
    //         {
    //             Email = _teacher.Email,
    //             Password = _password,
    //             UserName = _teacher.UserName,
    //             FirstName = _teacher.FirstName,
    //             LastName = _teacher.LastName,
    //             DateOfBirth = _teacher.DateOfBirth,
    //             IsTeacher = false
    //         };

    //         // Act
    //         var authModel = await _authService.RegisterUserAsync(registerDto);

    //         // Assert
    //         Assert.True(authModel.IsSucceed);
    //         Assert.Null(authModel.Errors);
    //         Assert.Equal(_teacher.Email, authModel.Email);
    //         Assert.Equal(_teacher.UserName, authModel.UserName);
    //         Assert.Equal(_teacher.FirstName, authModel.FirstName);
    //         Assert.NotNull(authModel.Roles);
    //         Assert.Single(authModel.Roles);
    //         Assert.Contains(Roles.Teacher, authModel.Roles);

    //         _userManagerMock
    //             .Verify(mock => mock.CreateAsync(It.Is<ApplicationUser>(u => u.Email == _teacher.Email)).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(mock => mock.AddPasswordAsync(It.Is<ApplicationUser>(u => u.Email == _teacher.Email), _password).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(mock => mock.AddToRoleAsync(It.Is<ApplicationUser>(u => u.Email == _teacher.Email), Roles.Teacher).Result, Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_RegisterDuplicatedStudent_ShouldReturnNotSucceedAuthDto()
    //     {
    //         // Arrange
    //         RegisterDto registerDto = new()
    //         {
    //             Email = _duplicatedStudent.Email,
    //             Password = _password,
    //             UserName = _duplicatedStudent.UserName,
    //             FirstName = _duplicatedStudent.FirstName,
    //             LastName = _duplicatedStudent.LastName,
    //             DateOfBirth = _duplicatedStudent.DateOfBirth,
    //             IsTeacher = false
    //         };
    //         _userManagerMock
    //             .Setup(m => m.FindByEmailAsync(_duplicatedStudent.Email).Result)
    //             .Returns(_duplicatedStudent);

    //         // Act
    //         var authModel = await _authService.RegisterUserAsync(registerDto);

    //         // Assert
    //         Assert.False(authModel.IsSucceed);
    //         Assert.NotNull(authModel.Errors);
    //         Assert.Null(authModel.Email);
    //         Assert.Null(authModel.UserName);
    //         Assert.Null(authModel.FirstName);
    //         Assert.Null(authModel.Roles);
    //         Assert.Null(authModel.Roles);

    //         _userManagerMock
    //             .Verify(mock => mock.FindByEmailAsync(_student.Email).Result, Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_RegisterInvalidPasswordStudent_ShouldReturnNotSucceedAuthDto()
    //     {
    //         // Arrange
    //         RegisterDto registerDto = new()
    //         {
    //             Email = _student.Email,
    //             Password = "helloworld",
    //             UserName = _student.UserName,
    //             FirstName = _student.FirstName,
    //             LastName = _student.LastName,
    //             DateOfBirth = _student.DateOfBirth,
    //             IsTeacher = false
    //         };

    //         // Act
    //         var authModel = await _authService.RegisterUserAsync(registerDto);

    //         // Assert
    //         Assert.False(authModel.IsSucceed);
    //         Assert.NotNull(authModel.Errors);
    //         Assert.Null(authModel.Email);
    //         Assert.Null(authModel.UserName);
    //         Assert.Null(authModel.FirstName);
    //         Assert.Null(authModel.Roles);
    //         Assert.Null(authModel.Roles);

    //         _userManagerMock
    //             .Verify(mock => mock.CreateAsync(It.Is<ApplicationUser>(u => u.Email == _student.Email)).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(mock => mock.AddPasswordAsync(It.Is<ApplicationUser>(u => u.Email == _student.Email), _password).Result, Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_GetToken_ShouldReturnSucceedAuthDto()
    //     {
    //         // Arrange
    //         _userManagerMock
    //             .Setup(m => m.FindByEmailAsync(It.IsAny<string>()).Result)
    //             .Returns(_student);

    //         _userManagerMock
    //             .Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result)
    //             .Returns(true);

    //         // Act
    //         var authModel = await _authService.GetTokenAsync(_student);

    //         // Assert
    //         Assert.True(authModel.IsSucceed);
    //         Assert.Null(authModel.Errors);
    //         Assert.Equal(_student.Email, authModel.Email);
    //         Assert.Equal(_student.UserName, authModel.UserName);
    //         Assert.Equal(_student.FirstName, authModel.FirstName);
    //         Assert.NotNull(authModel.Roles);
    //         Assert.Single(authModel.Roles);
    //         Assert.Contains(Roles.Student, authModel.Roles);

    //         _userManagerMock
    //             .Verify(m => m.FindByEmailAsync(_student.Email), Times.Once);
    //         _userManagerMock
    //             .Verify(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    //         _jwtSecurityTokenHandlerMock
    //             .Verify(m => m.WriteToken(It.IsAny<JwtSecurityToken>()), Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_GetTokenWithInvalidPassword_ShouldReturnNotSucceedAuthDto()
    //     {
    //         // Arrange
    //         _userManagerMock
    //             .Setup(m => m.FindByEmailAsync(It.IsAny<string>()).Result)
    //             .Returns(_student);

    //         _userManagerMock
    //             .Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()).Result)
    //             .Returns(false);

    //         // Act
    //         var authModel = await _authService.GetTokenAsync(_student);

    //         // Assert
    //         Assert.False(authModel.IsSucceed);
    //         Assert.NotNull(authModel.Errors);
    //         Assert.Null(authModel.Email);
    //         Assert.Null(authModel.UserName);
    //         Assert.Null(authModel.FirstName);
    //         Assert.Null(authModel.Roles);
    //         Assert.Null(authModel.Roles);

    //         _userManagerMock
    //             .Verify(m => m.FindByEmailAsync(_student.Email), Times.Once);
    //         _userManagerMock
    //             .Verify(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    //         _jwtSecurityTokenHandlerMock
    //             .Verify(m => m.WriteToken(It.IsAny<JwtSecurityToken>()), Times.Never);
    //     }

    //     [Fact]
    //     public async Task AuthService_RefreshToken_ShouldReturnSucceedAuthDto()
    //     {
    //         // Arrange
    //         RefreshTokenDto refreshTokenDto = new()
    //         {
    //             RefreshToken = new Guid().ToString()
    //         };
    //         _userManagerMock
    //             .Setup(m => m.Users.FirstOrDefault(It.IsAny<Func<ApplicationUser, bool>>()))
    //             .Returns(_student);

    //         // Act
    //         var authModel = await _authService.RefreshTokenAsync(refreshTokenDto);

    //         // Assert
    //         Assert.True(authModel.IsSucceed);
    //         Assert.Null(authModel.Errors);

    //         _userManagerMock
    //             .Verify(m => m.UpdateAsync(It.IsAny<ApplicationUser>()).Result, Times.Once);
    //         _jwtSecurityTokenHandlerMock
    //             .Verify(m => m.WriteToken(It.IsAny<JwtSecurityToken>()), Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_UpdateUser_ShouldReturnSucceedAuthDto()
    //     {
    //         // Arrange
    //         UpdateUserDto updateDto = new()
    //         {
    //             Id = new Guid().ToString(),
    //             Email = _student.Email,
    //             UserName = _student.UserName,
    //             DateOfBirth = _student.DateOfBirth,
    //             IsTeacher = false
    //         };

    //         // Act
    //         var authModel = await _authService.UpdateUserAsync(updateDto);

    //         // Assert
    //         Assert.True(authModel.IsSucceed);
    //         Assert.Null(authModel.Errors);
    //         Assert.Equal(_student.Email, authModel.Email);
    //         Assert.Equal(_student.UserName, authModel.UserName);
    //         Assert.Equal(_student.FirstName, authModel.FirstName);
    //         Assert.NotNull(authModel.Roles);
    //         Assert.Single(authModel.Roles);
    //         Assert.Contains(Roles.Student, authModel.Roles);


    //         _userManagerMock
    //             .Verify(m => m.FindByIdAsync(It.IsAny<string>()).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(m => m.UpdateAsync(It.IsAny<ApplicationUser>()).Result, Times.Once);
    //     }

    //     [Fact]
    //     public async Task AuthService_DeactivateUser_ShouldReturnNoErrors()
    //     {
    //         // Arrange
    //         _userManagerMock
    //             .Setup(m => m.FindByIdAsync(It.IsAny<string>()).Result)
    //             .Returns(_student);

    //         // Act
    //         var authModel = await _authService.DeactivateUserAsync(new Guid().ToString());

    //         // Assert
    //         Assert.True(authModel.IsSucceed);
    //         Assert.Null(authModel.Errors);

    //         _userManagerMock
    //             .Verify(m => m.FindByIdAsync(It.IsAny<string>()).Result, Times.Once);
    //         _userManagerMock
    //             .Verify(m => m.UpdateAsync(It.IsAny<ApplicationUser>()).Result, Times.Once);
    //     }
    // }
}

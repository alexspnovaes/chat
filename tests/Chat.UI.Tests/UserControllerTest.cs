using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;
using Chat.UI.Controllers;
using Chat.UI.Models;
using Chat.UI.Tests.Builders;
using Chat.UI.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Chat.UI.Tests
{
    public class UserControllerTest
    {
        private UserController _userController;
        private Mock<IUserService> _mockUserService;
        private Mock<FakeSignInManager> _mockSignInManager;
        private RegisterViewModel _input;

        [TestInitialize]
        [Fact]
        public void TestInitialize()
        {
            // Arrange
            _input = new NewUserInputBuilder().Build();
            _mockUserService = new Mock<IUserService>();
            _mockSignInManager = new Mock<FakeSignInManager>();
            _userController = new UserController(_mockSignInManager.Object, _mockUserService.Object);
        }

        [Fact]
        public void ValidateModel_ReturnFalse_WhenInvavalidModelPosted_WrongEmail()
        {
            //Arrange
            TestInitialize();
            _input.Email = "wrongemail";
            var context = new ValidationContext(_input, null, null);
            var results = new List<ValidationResult>();


            //Act
            var isModelStateValid = Validator.TryValidateObject(_input, context, results, true);

            //Assert
            Xunit.Assert.False(isModelStateValid);
            Xunit.Assert.True(results.Any());
            Xunit.Assert.True(results.Count == 1);
            Xunit.Assert.Equal("Invalid Email", results.FirstOrDefault().ErrorMessage);
        }

        [Fact]
        public void ValidateModel_ReturnFalse_WhenInvavalidModelPosted_PasswordAndConfirmNotMatch()
        {
            //Arrange
            TestInitialize();
            _input.Password = "inva";
            _input.ConfirmPassword = "aa";
            var context = new ValidationContext(_input, null, null);
            var results = new List<ValidationResult>();


            //Act
            var isModelStateValid = Validator.TryValidateObject(_input, context, results, true);

            //Assert
            Xunit.Assert.False(isModelStateValid);
            Xunit.Assert.True(results.Any());
            Xunit.Assert.True(results.Count == 1);
            Xunit.Assert.Equal("Passwords do not match", results.FirstOrDefault().ErrorMessage);
        }

        [Fact]
        public async Task RegisterNewUser_RedirectToActionResult_WhenValidModelPosted()
        {
            //Arrange
            TestInitialize();

            _mockUserService.Setup(m => m.RegisterAsync(It.IsAny<UserModel>())).ReturnsAsync(IdentityResult.Success);

            //Act
            var actual = await _userController.Register(_input);

            //Assert
            var viewResult = Xunit.Assert.IsType<RedirectToActionResult>(actual);
        }
    }
}

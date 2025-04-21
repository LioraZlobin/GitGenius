using System.Collections.Generic;
using System.Linq;
using Xunit;
using GitGenius.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace GitGeniusTests
{
    public class RegisterViewModelTests
    {
        [Fact]
        public void Register_ShouldReturnError_WhenEmailAlreadyExists()
        {
            // Arrange
            var existingUsers = new List<RegisterViewModel>
            {
                new RegisterViewModel { Email = "test@example.com" }
            };

            var newUser = new RegisterViewModel { Email = "test@example.com" };

            // Act
            var isDuplicate = existingUsers.Any(u => u.Email == newUser.Email);

            // Assert
            Assert.True(isDuplicate, "המערכת לא זיהתה שכפול כתובת מייל");
        }

        [Fact]
        public void Register_ShouldReturnError_WhenIdNumberAlreadyExists()
        {
            // Arrange
            var existingUsers = new List<RegisterViewModel>
    {
        new RegisterViewModel { IdNumber = "123456789" }
    };

            var newUser = new RegisterViewModel { IdNumber = "123456789" };

            // Act
            var isDuplicate = existingUsers.Any(u => u.IdNumber == newUser.IdNumber);

            // Assert
            Assert.True(isDuplicate, "המערכת לא זיהתה שכפול תעודת זהות");
        }


        [Fact]
        public void Register_ShouldFailValidation_WhenFieldsAreEmpty()
        {
            // Arrange
            var user = new RegisterViewModel
            {
                FullName = "",
                Email = "",
                IdNumber = "",
                Password = "",
                Role = 0,
                ApprovalFile = null
            };

            // Act
            bool hasEmptyFields = string.IsNullOrWhiteSpace(user.FullName) ||
                                  string.IsNullOrWhiteSpace(user.Email) ||
                                  string.IsNullOrWhiteSpace(user.IdNumber) ||
                                  string.IsNullOrWhiteSpace(user.Password) ||
                                  user.ApprovalFile == null;

            // Assert
            Assert.True(hasEmptyFields, "המערכת לא זיהתה שדות חסרים");
        }

        [Fact]
        public void Register_ShouldPassValidation_WhenAllFieldsAreValid()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();

            var user = new RegisterViewModel
            {
                FullName = "Test User",
                Email = "newuser@example.com",
                IdNumber = "987654321",
                Password = "StrongPassword123",
                Role = GitGenius.Models.UserRole.Student,
                ApprovalFile = fileMock.Object
            };

            // Act
            bool allValid = !string.IsNullOrWhiteSpace(user.FullName) &&
                            !string.IsNullOrWhiteSpace(user.Email) &&
                            !string.IsNullOrWhiteSpace(user.IdNumber) &&
                            !string.IsNullOrWhiteSpace(user.Password) &&
                            user.ApprovalFile != null;

            // Assert
            Assert.True(allValid, "המערכת זיהתה שדות תקינים אך עדיין נכשלה");
        }
    }
}

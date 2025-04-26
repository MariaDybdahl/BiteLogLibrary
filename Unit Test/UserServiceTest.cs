using BiteLogLibrary.DTO;
using BiteLogLibrary.Helper;
using BiteLogLibrary.Interface.Repository;
using BiteLogLibrary.Interface.Services;
using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using BiteLogLibrary.Services;
using Moq;

namespace Unit_Test
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserRepository> _mockRepo;
        private PasswordHasher _passwordHasher;
        private UserService _userService;
        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IUserRepository>();
            _passwordHasher = new PasswordHasher(); // eller mock hvis nødvendigt
            _userService = new UserService(_mockRepo.Object, _passwordHasher);
        }

        [TestMethod]
        public async Task RegisterAsync_ThrowsException_IfEmailAlreadyExists()
        {
            // Arrange
            var fakeUser = new User { Email = "test@mail.com" };
            _mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
           .ReturnsAsync(fakeUser);

            var request = new RegisterRequest
            {
                Email = "test@mail.com",
                Username = "nyBruger",
                Password = "hemmelig123",
                Height = 50,
                Weight = 50,
                DateOfBirth = new DateTime(2000, 3, 19),
                FirstName = "Jens",
                LastName = "Ole",
                Gender = "Man"
            };

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _userService.RegisterAsync(request)
            );

            Assert.AreEqual("Email already exists", ex.Message);
        }
        [TestMethod]
        public async Task RegisterAsync_ReturnsEmail_IfEmailDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
           .ReturnsAsync((User u) => u);


            var request = new RegisterRequest
            {
                Email = "NotTheSame@mail.com",
                Username = "nyBrugefr",
                Password = "hemmelig123",
                Height = 50,
                Weight = 50,
                DateOfBirth = new DateTime(2000, 3, 19),
                FirstName = "Jens",
                LastName = "Ole",
                Gender = "Man"
            };

            // Act
            var result = await _userService.RegisterAsync(request);
           

            // Assert
           Assert.AreEqual("NotTheSame@mail.com", result.Email);

        }


    }
}
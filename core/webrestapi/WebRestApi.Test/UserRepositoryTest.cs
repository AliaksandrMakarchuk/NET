using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebRestApi.Models;
using WebRestApi.Repository;

namespace WebRestApi.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UserRepositoryBase _userRepository;
        private IList<User> _users;
        private Mock<DbSet<User>> _usersMock;
        private Mock<WebRestApiContext> _contextMock;

        [TestInitialize]
        public void Initialization()
        {
            var optionsBuilder = new Mock<DbContextOptionsBuilder<WebRestApiContext>>();
            optionsBuilder.Setup(o => o.IsConfigured).Returns(true);
            optionsBuilder.Setup(o => o.Options).Returns(new DbContextOptions<WebRestApiContext>());

            var databaseMock = new Mock<DatabaseFacade>(new DbContext(new DbContextOptionsBuilder().Options));
            databaseMock.Setup(db => db.EnsureCreated()).Returns(true);

            _contextMock = new Mock<WebRestApiContext>(optionsBuilder.Object.Options);
            _contextMock.Setup(c => c.Database).Returns(databaseMock.Object);

            _usersMock = new Mock<DbSet<User>>();
            _contextMock.Setup(x => x.Users).Returns(_usersMock.Object);

            _userRepository = new UserRepository(_contextMock.Object);

            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName= "First",
                    LastName= "Last",
                    IsAuthorized = false
                },
                new User
                {
                    Id = 2,
                    FirstName = "Second",
                    LastName = "Last",
                    IsAuthorized = false
                }
            };
        }

        [TestMethod]
        public async Task GetAllUsers_Test()
        {
            var users = await _userRepository.GetAllAsync();

            _usersMock.Verify(u => u.Local, Times.Once);
        }

        [TestMethod]
        public async Task GetUserById_Test()
        {
            var user = await _userRepository.GetByIdAsync(1);

            _usersMock.Verify(u => u.FindAsync(It.Is<object[]>(keys => keys.Length == 1 && (int)keys[0] == 1)));
        }

        [TestMethod]
        public async Task AddUser_Test()
        {
            var newUser = new User
            {
                Id = 3,
                FirstName = "Third",
                LastName = "Last2",
                IsAuthorized = false
            };

            await _userRepository.AddAsync(newUser);

            _usersMock.Verify(u => u.Add(It.Is<User>(x => x.Equals(newUser))));
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task UpdateUser_Null_DontRemove_Test()
        {
            await _userRepository.UpdateAsync(null);

            _contextMock.Verify(u => u.Update(It.IsAny<User>()), Times.Never);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdateUser_NotFound_DontRemove_Test()
        {

            var user = _users.First();

            var newUser = new User
            {
                Id = user.Id,
                FirstName = "OneFirst",
                LastName = "OneLast",
                IsAuthorized = true
            };

            _usersMock.Setup(u => u.FindAsync(It.IsAny<object[]>())).Returns(Task.FromResult<User>(null));

            await _userRepository.UpdateAsync(newUser);

            _contextMock.Verify(u => u.Update(It.IsAny<User>()), Times.Never);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task UpdateUser_Test()
        {

            var user = _users.First();

            var newUser = new User
            {
                Id = user.Id,
                FirstName = "OneFirst",
                LastName = "OneLast",
                IsAuthorized = true
            };

            _usersMock.Setup(u => u.FindAsync(It.IsAny<object[]>())).Returns(Task.FromResult(user));

            await _userRepository.UpdateAsync(newUser);

            _contextMock.Verify(u => u.Update(It.Is<User>(x => x.Equals(newUser))));
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [TestMethod]
        public async Task DeleteUser_Null_DontRemove_Test()
        {
            await _userRepository.DeleteAsync(null);
            _usersMock.Verify(u => u.FindAsync(It.IsAny<object[]>()), Times.Never);
            _usersMock.Verify(u => u.Remove(It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteUser_NotFound_DontRemove_Test()
        {
            var user = _users.First();
            _usersMock.Setup(u => u.FindAsync(It.IsAny<object[]>())).Returns(Task.FromResult<User>(null));

            var removedUser = await _userRepository.DeleteAsync(user);

            _usersMock.Verify(u => u.FindAsync(It.IsAny<object[]>()), Times.Once);
            _usersMock.Verify(u => u.Remove(It.IsAny<User>()), Times.Never);
            Assert.IsNull(removedUser);
        }

        [TestMethod]
        public async Task DeleteUser_Test()
        {
            var user = _users.First();
            _usersMock.Setup(u => u.FindAsync(It.IsAny<object[]>())).Returns(Task.FromResult(user));

            var removedUser = await _userRepository.DeleteAsync(user);

            Assert.IsNotNull(removedUser);
            _usersMock.Verify(u => u.FindAsync(It.Is<object[]>(keys => keys.Length == 1 && (int)keys[0] == user.Id)));
            _usersMock.Verify(u => u.Remove(It.Is<User>(x => x == user)));
        }
    }
}

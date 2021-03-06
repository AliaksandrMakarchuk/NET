using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebRestApi.Service;
using WebRestApi.Service.Repository;
using WebRestApi.Service.Models;

namespace WebRestApi.Service.Tests
{
    [TestClass]
    public class DataServiceTest
    {
        private IDataService _dataService;
        private Mock<UserRepositoryBase> _userRepositoryMock;
        private Mock<RoleRepositoryBase> _roleRepositoryMock;
        private Mock<MessageRepositoryBase> _messageRepositoryMock;
        private Mock<CommentRepositoryBase> _commentRepositoryMock;
        private IList<User> _users;

        [TestInitialize]
        public void Initialization()
        {
            var optionsBuilder = new Mock<DbContextOptionsBuilder<AbstractDbContext>>();
            optionsBuilder.Setup(o => o.IsConfigured).Returns(true);
            optionsBuilder.Setup(o => o.Options).Returns(new DbContextOptions<AbstractDbContext>());

            var databaseMock = new Mock<DatabaseFacade>(new DbContext(new DbContextOptionsBuilder().Options));
            databaseMock.Setup(db => db.EnsureCreated()).Returns(true);

            var contextMock = new Mock<AbstractDbContext>(optionsBuilder.Object.Options);
            contextMock.Setup(c => c.Database).Returns(databaseMock.Object);

            _userRepositoryMock = new Mock<UserRepositoryBase>(contextMock.Object);
            _roleRepositoryMock = new Mock<RoleRepositoryBase>(contextMock.Object);
            _messageRepositoryMock = new Mock<MessageRepositoryBase>(contextMock.Object);
            _commentRepositoryMock = new Mock<CommentRepositoryBase>(contextMock.Object);
            _dataService = new DataService(
                _userRepositoryMock.Object,
                _roleRepositoryMock.Object,
                _messageRepositoryMock.Object,
                _commentRepositoryMock.Object
                );

            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName= "First",
                    LastName= "Last",
                },
                new User
                {
                    Id = 2,
                    FirstName = "Second",
                    LastName = "Last",
                }
            };
        }

        [TestMethod]
        public async Task GetAllUsers_Test()
        {
            await _dataService.GetAllUsers();

            _userRepositoryMock.Verify(u => u.GetAllAsync());
        }

        [TestMethod]
        public async Task GetUserById_Test()
        {
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns<int>((id) => Task.FromResult(_users.SingleOrDefault(x => x.Id == id)));

            var user = await _dataService.GetUserByIdAsync(1);

            Assert.IsNotNull(user);
            Assert.AreEqual(_users[0], user);
        }

        [TestMethod]
        public async Task GetUserById_UserNotFound_Test()
        {
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns<int>((id) => Task.FromResult(_users.SingleOrDefault(x => x.Id == id)));

            var user = await _dataService.GetUserByIdAsync(3);

            Assert.IsNull(user);
        }

        // [TestMethod]
        // public async Task GetUsersByName_Test()
        // {
        //     _userRepositoryMock.Setup(x => x.GetByLoginPassword(It.IsAny<string>()))
        //         .Returns<string>((userName) => Task.FromResult(_users.Where(x => x.FirstName.Contains(userName) ||
        //         x.LastName.Contains(userName) || userName.Contains(x.FirstName) || userName.Contains(x.LastName))));

        //     var users = await _dataService.GetUserByLoginPassword("First");

        //     Assert.AreEqual(1, users.Count());
        //     Assert.AreEqual(_users[0], users.First());
        // }

        [TestMethod]
        public async Task CreateNewUser_Test()
        {
            User user = null;
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Callback<User>(x => user = x);

            await _dataService.CreateNewUserAsync(new Models.Client.IdentityUser() { FirstName = "fn", LastName = "ln"});

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()));
            Assert.IsNotNull(user);
            Assert.AreEqual("fn", user.FirstName);
            Assert.AreEqual("ln", user.LastName);
        }

        [TestMethod]
        public async Task UpdateUser_Test()
        {
            User user = new User { Id = 1, FirstName = "fn", LastName = "ln" };
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(id => id == 1))).Returns<int>(id => Task.FromResult(user));
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).Returns<User>(u => Task.FromResult(u));

            var updatedUser = await _dataService.UpdateUserAsync(new Models.Client.ClientUser() {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            });

            _userRepositoryMock.Verify(x => x.UpdateAsync(It.Is<User>(u => u == user)));
        }
    }
}

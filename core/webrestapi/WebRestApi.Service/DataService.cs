using System.Collections.Generic;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Repository;

namespace WebRestApi.Service
{
    public class DataService : IDataService
    {
        private readonly UserRepositoryBase _userRepository;
        private readonly MessageRepositoryBase _messageRepository;
        private readonly CommentRepositoryBase _commentRepository;

        public DataService(
            UserRepositoryBase userRepository,
            MessageRepositoryBase messageRepository,
            CommentRepositoryBase commentRepository
            )
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersByName(string userName)
        {
            return await _userRepository.GetByNameAsync(userName);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return await _userRepository.DeleteAsync(user);
        }

        public async Task<User> CreateNewUser(string firstName, string lastName)
        {
            return await _userRepository.AddAsync(new User
            {
                FirstName = firstName,
                LastName = lastName
            });
        }
    }
}

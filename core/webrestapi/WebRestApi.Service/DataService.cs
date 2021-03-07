using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;
using WebRestApi.Service.Repository;

namespace WebRestApi.Service
{
    public class DataService : IDataService
    {
        private readonly UserRepositoryBase _userRepository;
        private readonly RoleRepositoryBase _roleRepository;
        private readonly MessageRepositoryBase _messageRepository;
        private readonly CommentRepositoryBase _commentRepository;

        public DataService(
            UserRepositoryBase userRepository,
            RoleRepositoryBase roleRepository,
            MessageRepositoryBase messageRepository,
            CommentRepositoryBase commentRepository
        )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _messageRepository = messageRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login, string password)
        {
            return await _userRepository.GetByLoginPasswordAsync(login, password);
        }

        public async Task<User> UpdateUserAsync(ClientUser user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id.Value);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            return await _userRepository.UpdateAsync(existingUser);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return await _userRepository.DeleteAsync(user);
        }

        public async Task<User> CreateNewUserAsync(IdentityUser user)
        {
            var role = await _roleRepository.GetByUserRole(UserRole.USER);

            return await _userRepository.AddAsync(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Login,
                Role = role,
                RoleId = role.Id,
                Password = user.Password
            });
        }

        public async Task<Message> GetMessageById(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            message.Sender = await _userRepository.GetByIdAsync(message.SenderId);
            message.Receiver = await _userRepository.GetByIdAsync(message.ReceiverId);

            return message;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync(ClaimsPrincipal user)
        {
            var allMessages = await _messageRepository.GetAllAsync();

            foreach (var message in allMessages)
            {
                message.Sender = await _userRepository.GetByIdAsync(message.SenderId);
                message.Receiver = await _userRepository.GetByIdAsync(message.ReceiverId);
            }

            if (user.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == UserRole.ADMIN.RoleName)
            {
                return allMessages;
            }

            var userId = int.Parse(user.FindFirst(x => x.Type == ClaimTypes.Sid).Value);

            return allMessages.Where(x => x.SenderId == userId);
        }

        public async Task<bool> SendMessageAsync(ClientMessage message)
        {
            var msg = await _messageRepository.AddAsync(new Message
            {
                SenderId = message.From.Id.Value,
                Sender = await _userRepository.GetByIdAsync(message.From.Id.Value),
                ReceiverId = message.To.Id.Value,
                Receiver = await _userRepository.GetByIdAsync(message.To.Id.Value),
                Text = message.Message
            });

            return msg != null;
        }

        public async Task<Message> UpdateMessageAsync(ClientMessage message)
        {
            var msg = await _messageRepository.GetByIdAsync(message.Id);

            if (msg == null)
            {
                return null;
            }

            msg.Text = message.Message;

            return await _messageRepository.UpdateAsync(msg);
        }

        public async Task<Message> DeleteMessageAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if (message == null)
            {
                return null;
            }

            return await _messageRepository.DeleteAsync(message);
        }
    }
}
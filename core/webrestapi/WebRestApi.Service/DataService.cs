using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;
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

        public async Task<IEnumerable<ClientUser>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new ClientUser
            {
                Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    RoleName = u.Role.Name
            });
        }

        public async Task<ClientUser> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return new ClientUser
            {
                Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleName = user.Role.Name
            };
        }

        public async Task<IEnumerable<ClientUser>> GetUsersByNameAsync(string userName)
        {
            var users = await _userRepository.GetByNameAsync(userName);

            return users.Select(u => new ClientUser
            {
                Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    RoleName = u.Role.Name
            });
        }

        public async Task<ClientUser> UpdateUserAsync(ClientUser user)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(user.Id);

                if (existingUser == null)
                {
                    return null;
                }

                var usr = await _userRepository.UpdateAsync(new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });

                return new ClientUser
                {
                    Id = usr.Id,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    RoleName = usr.Role.Name
                };
            }
            catch (Exception ex)
            {
                /// TODO: add logging
                throw ex;
            }

        }

        public async Task<ClientUser> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var usr = await _userRepository.DeleteAsync(user);

            return new ClientUser
                {
                    Id = usr.Id,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    RoleName = usr.Role.Name
                };
        }

        public async Task<ClientUser> CreateNewUserAsync(IdentityUser user)
        {
            var usr = await _userRepository.AddAsync(new User
            {
                FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Login,
                    Password = user.Password
            });

            return new ClientUser
                {
                    Id = usr.Id,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    RoleName = usr.Role.Name
                };
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _messageRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _messageRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessagesByUserAsync(int id)
        {
            var allMessages = await _messageRepository.GetAllAsync();
            return allMessages.Where(x => x.ReceiverId == id);
        }

        public async Task<bool> SendMessageAsync(ClientMessage message)
        {
            var msg = await _messageRepository.AddAsync(new Message
            {
                SenderId = message.FromId,
                    // Sender = await _userRepository.GetByIdAsync(message.FromId),
                    ReceiverId = message.ToId,
                    // Receiver = await _userRepository.GetByIdAsync(message.ToId),
                    Text = message.Message
            });

            return msg != null;
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
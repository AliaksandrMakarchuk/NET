using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string userName)
        {
            return await _userRepository.GetByNameAsync(userName);
        }

        public async Task<User> UpdateUserAsync(ClientUser user)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(user.Id);

                if (existingUser == null)
                {
                    return null;
                }

                return await _userRepository.UpdateAsync(new User
                {
                    Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                });
            }
            catch (Exception ex)
            {
                /// TODO: add logging
                throw ex;
            }

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

        public async Task<User> CreateNewUserAsync(ClientUser user)
        {
            return await _userRepository.AddAsync(new User
            {
                FirstName = user.FirstName,
                    LastName = user.LastName
            });
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await _messageRepository.GetAllAsync();
        }

        public async Task<bool> SendMessageAsync(ClientMessage message)
        {
            var msg = await _messageRepository.AddAsync(new Message
            {
                SenderId = message.FromId,
                    Sender = await _userRepository.GetByIdAsync(message.FromId),
                    ReceiverId = message.ToId,
                    Receiver = await _userRepository.GetByIdAsync(message.ToId),
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
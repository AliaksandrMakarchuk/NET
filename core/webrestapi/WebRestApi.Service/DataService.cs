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
                var existingUser = await _userRepository.GetByIdAsync(user.Id.Value);

                if (existingUser == null)
                {
                    return null;
                }

                var usr = await _userRepository.UpdateAsync(new User
                {
                    Id = user.Id.Value,
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

        public async Task<ClientMessage> UpdateMessageAsync(ClientMessage message)
        {
            var msg = await _messageRepository.GetByIdAsync(message.Id);

            if (msg == null)
            {
                return null;
            }

            msg.Text = message.Message;

            msg = await _messageRepository.UpdateAsync(msg);

            return new ClientMessage
            {
                Id = msg.Id,
                From = message.From ?? ToClientUser(await _userRepository.GetByIdAsync(msg.SenderId)),
                To = message.To ?? ToClientUser(await _userRepository.GetByIdAsync(msg.ReceiverId)),
                Message = message.Message
            };
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

        private ClientUser ToClientUser(User user) {
            return new ClientUser {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleName = user.Role.Name
            };
        }
    }
}
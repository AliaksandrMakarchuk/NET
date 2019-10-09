using Microsoft.AspNetCore.Mvc;
using WebRestApi.Models;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebRestApi.Services;

namespace WebRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ILogger _logger;
        private IDataService _dataService;

        public UsersController(
            ILogger<UsersController> logger,
            IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LoggingEvents.GetAllUsers, "Getting all existing users");

            return Ok(await _dataService.GetAllUsers());
        }

        // GET: api/users/id=5
        [HttpGet("id={id}", Name = "GetById")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetUserById, "Get User by Id {0}", id);

            var user = await _dataService.GetUserById(id);

            if (user == null)
            {
                return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
            }

            return Ok(user);
        }

        // GET: api/users/userName=alexander
        [HttpGet("userName={userName}", Name = "GetByName")]
        public async Task<IActionResult> Get(string userName)
        {
            _logger.LogInformation(LoggingEvents.GetUsersByName, $"Get Users by Name '{userName}'");

            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest(new ErrorResponse { Message = "Wrong user name" });
            }

            var users = await _dataService.GetUsersByName(userName);

            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if (string.IsNullOrWhiteSpace(user?.FirstName) || string.IsNullOrWhiteSpace(user?.LastName))
            {
                return BadRequest(new ErrorResponse { Message = "User first name and last name should be filled in" });
            }

            try
            {
                await _dataService.CreateNewUser(user.FirstName, user.LastName);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on creating new User.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return BadRequest(new ErrorResponse { Message = "Error has accured on saving changes" });
            }

            return Ok();
        }

        // POST: api/users/3
        [HttpPost("{senderId}")]
        public IActionResult Post(int fromUserId, Message message)
        {
            _logger.LogInformation(LoggingEvents.SendMessage, $"Send message from User with Id {fromUserId} to User with Id {message?.Id}");

            if (message == null)
            {
                return BadRequest(new ErrorResponse { Message = "Wrong body" });
            }

            if (string.IsNullOrWhiteSpace(message.Text))
            {
                return BadRequest(new ErrorResponse { Message = "Message text could not be empty. Please, fill in it." });
            }

            var sender = _dataService.GetUserById(fromUserId);

            if (sender == null)
            {
                _logger.LogError(LoggingEvents.GetUserById, $"Could not find User with Id {fromUserId}");
                return BadRequest(new ErrorResponse { Message = "Wrong sender Id" });
            }

            //if (!sender.IsAuthorized)
            //{
            //    return BadRequest(new ErrorResponse { Message = "Sender User is not authorized. Please, authorize the User to send messages." });
            //}

            // var receiver = _dataService.GetUserById(message.ReceiverId.Value);

            // if (receiver == null)
            // {
            //     _logger.LogError(LoggingEvents.GetUserById, $"Could not find User with Id {message.ReceiverId}");
            //     return BadRequest(new ErrorResponse { Message = "Wrong receiver Id in body" });
            // }

            //message.SenderId = sender.Id;
            //message.Sender = sender;
            //message.Receiver = receiver;

            //if (sender.SendMessages == null)
            //{
            //    sender.SendMessages = new List<Message>();
            //}

            //if (sender.ReceivedMessages == null)
            //{
            //    sender.ReceivedMessages = new List<Message>();
            //}

            //sender.SendMessages.Add(message);

            //if (receiver.SendMessages == null)
            //{
            //    receiver.SendMessages = new List<Message>();
            //}

            //if (receiver.ReceivedMessages == null)
            //{
            //    receiver.ReceivedMessages = new List<Message>();
            //}

            //receiver.ReceivedMessages.Add(message);

            //try
            //{
            //    _messageRepository.Add(message);
            //    _userRepository.Update(sender);
            //    _userRepository.Update(receiver);
            //}
            //catch (DbUpdateException ex)
            //{
            //    _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on saving changes.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
            //    return BadRequest(new ErrorResponse { Message = "Error has accured on saving changes" });
            //}

            return Ok();
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            _logger.LogInformation(LoggingEvents.GetUserById, $"Update User with Id {id}. Set IsAuthorized: {user.IsAuthorized}");

            var existingUser = await _dataService.GetUserById(id);

            if (existingUser == null)
            {
                return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
            }

            existingUser.IsAuthorized = user.IsAuthorized;

            try
            {
                await _dataService.UpdateUser(existingUser);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on authorizing User with Id {id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return BadRequest(new ErrorResponse { Message = "Error has accured on saving changes" });
            }

            return Ok();
        }

        // PUT: api/users
        [HttpPut]
        public IActionResult Put(User user)
        {
            _logger.LogInformation(LoggingEvents.UpdateUserName, $"Update User with Id {user.Id}. Set User: {user}");

            var existingUser = _dataService.GetUserById(user.Id);

            //if (existingUser == null)
            //{
            //    return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
            //}

            //if (!string.IsNullOrWhiteSpace(user.FirstName))
            //{
            //    existingUser.FirstName = user.FirstName;
            //}

            //if (!string.IsNullOrWhiteSpace(user.LastName))
            //{
            //    existingUser.LastName = user.LastName;
            //}

            try
            {
                //_userRepository.Update(existingUser);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on updating User with Id {user.Id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return BadRequest(new ErrorResponse { Message = "Error has accured on saving changes" });
            }

            return Ok();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteUser, $"Delete User with Id {id}");

            try
            {
                //_userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnDeletingUser, $"Error on deleting User with Id {id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return BadRequest(new ErrorResponse { Message = "Error on removing specified User" });
            }

            return Ok();
        }
    }
}

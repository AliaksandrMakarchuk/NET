using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebRestApi.Service;
using WebRestApi.Service.Models;

namespace WebRestApi.Controllers
{
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ILogger _logger;
        private IDataService _dataService;

        ///
        public UsersController(
            ILogger<UsersController> logger,
            IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <remarks></remarks>
        /// <returns>Collection of existing users</returns>
        /// <response code="200">If operation has been completed without any exception</response>
        /// <response code="500">If something wrong had happen during getting users</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LoggingEvents.GetAllUsers, "Getting all existing users");

            try
            {
                var users = await _dataService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Something went wrong" });
            }
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id"></param>
        /// <returns>User by Id</returns>
        /// <response code="200">If operation has been completed without any exception</response>
        /// <response code="500">If something wrong had happen during gettting the user</response>
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetUserById, "Get User by Id {0}", id);

            try
            {
                var user = await _dataService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Something went wrong" });
            }
        }

        // // GET: api/users/userName=alexander
        // [HttpGet("userName={userName}", Name = "GetByName")]
        // public async Task<IActionResult> Get(string userName)
        // {
        //     _logger.LogInformation(LoggingEvents.GetUsersByName, $"Get Users by Name '{userName}'");

        //     if (string.IsNullOrWhiteSpace(userName))
        //     {
        //         return BadRequest(new ErrorResponse { Message = "Wrong user name" });
        //     }

        //     var users = await _dataService.GetUsersByName(userName);

        //     return Ok(users);
        // }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="user">User</param>
        /// <returns>New created User</returns>
        /// <response code="201">If new User has been successfully created</response>
        /// <response code="400">If the First or Last name was not specified</response>
        /// <response code="500">If something wrong had happen during the User creation</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                return BadRequest(new ErrorResponse { Message = "User first name and last name should be filled in" });
            }

            try
            {
                var newUser = await _dataService.CreateNewUser(user.FirstName, user.LastName);
                return Created("here", newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on creating new User.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                var error = new ErrorResponse { Message = "Error has accured on saving changes" };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        // // POST: api/users/3
        // [HttpPost("{senderId}")]
        // public IActionResult Post(int fromUserId, Message message)
        // {
        //     _logger.LogInformation(LoggingEvents.SendMessage, $"Send message from User with Id {fromUserId} to User with Id {message?.Id}");

        //     if (message == null)
        //     {
        //         return BadRequest(new ErrorResponse { Message = "Wrong body" });
        //     }

        //     if (string.IsNullOrWhiteSpace(message.Text))
        //     {
        //         return BadRequest(new ErrorResponse { Message = "Message text could not be empty. Please, fill in it." });
        //     }

        //     var sender = _dataService.GetUserById(fromUserId);

        //     if (sender == null)
        //     {
        //         _logger.LogError(LoggingEvents.GetUserById, $"Could not find User with Id {fromUserId}");
        //         return BadRequest(new ErrorResponse { Message = "Wrong sender Id" });
        //     }

        //     //if (!sender.IsAuthorized)
        //     //{
        //     //    return BadRequest(new ErrorResponse { Message = "Sender User is not authorized. Please, authorize the User to send messages." });
        //     //}

        //     // var receiver = _dataService.GetUserById(message.ReceiverId.Value);

        //     // if (receiver == null)
        //     // {
        //     //     _logger.LogError(LoggingEvents.GetUserById, $"Could not find User with Id {message.ReceiverId}");
        //     //     return BadRequest(new ErrorResponse { Message = "Wrong receiver Id in body" });
        //     // }

        //     //message.SenderId = sender.Id;
        //     //message.Sender = sender;
        //     //message.Receiver = receiver;

        //     //if (sender.SendMessages == null)
        //     //{
        //     //    sender.SendMessages = new List<Message>();
        //     //}

        //     //if (sender.ReceivedMessages == null)
        //     //{
        //     //    sender.ReceivedMessages = new List<Message>();
        //     //}

        //     //sender.SendMessages.Add(message);

        //     //if (receiver.SendMessages == null)
        //     //{
        //     //    receiver.SendMessages = new List<Message>();
        //     //}

        //     //if (receiver.ReceivedMessages == null)
        //     //{
        //     //    receiver.ReceivedMessages = new List<Message>();
        //     //}

        //     //receiver.ReceivedMessages.Add(message);

        //     //try
        //     //{
        //     //    _messageRepository.Add(message);
        //     //    _userRepository.Update(sender);
        //     //    _userRepository.Update(receiver);
        //     //}
        //     //catch (DbUpdateException ex)
        //     //{
        //     //    _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on saving changes.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
        //     //    return BadRequest(new ErrorResponse { Message = "Error has accured on saving changes" });
        //     //}

        //     return Ok();
        // }

        /// <summary>
        /// Update user
        /// </summary>
        /// <remarks></remarks>
        /// <param name="user">User with new properties</param>
        /// <returns>User updated parameters</returns>
        /// <response code="200">If the User successfully updated</response>
        /// <response code="400">If could not find User by User.Id</response>
        /// <response code="500">If something wrong had happend during user update</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            _logger.LogInformation(LoggingEvents.GetUserById, $"Update User with Id {user.Id}");

            try
            {
                var existingUser = await _dataService.GetUserById(user.Id);

                if (existingUser == null)
                {
                    return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
                }

                var updatedUser = await _dataService.UpdateUser(existingUser);
                return Ok(updatedUser);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on authorizing User with Id {user.Id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Error has accured on saving changes" });
            }
        }

        // // PUT: api/users
        // [HttpPut]
        // public IActionResult Put(User user)
        // {
        //     _logger.LogInformation(LoggingEvents.UpdateUserName, $"Update User with Id {user.Id}. Set User: {user}");

        //     var existingUser = _dataService.GetUserById(user.Id);

        //     //if (existingUser == null)
        //     //{
        //     //    return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
        //     //}

        //     //if (!string.IsNullOrWhiteSpace(user.FirstName))
        //     //{
        //     //    existingUser.FirstName = user.FirstName;
        //     //}

        //     //if (!string.IsNullOrWhiteSpace(user.LastName))
        //     //{
        //     //    existingUser.LastName = user.LastName;
        //     //}

        //     try
        //     {
        //         //_userRepository.Update(existingUser);
        //     }
        //     catch (DbUpdateException ex)
        //     {
        //         _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on updating User with Id {user.Id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
        //         return BadRequest(new ErrorResponse { Message = "Error has accured on saving changes" });
        //     }

        //     return Ok();
        // }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <remarks></remarks>
        /// <response code="200">If User has heen successfully deleted</response>
        /// <response code="500">If something went wrong during removing the User</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromBody] int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteUser, $"Delete User with Id {id}");

            try
            {
                _dataService.DeleteUser(id);
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

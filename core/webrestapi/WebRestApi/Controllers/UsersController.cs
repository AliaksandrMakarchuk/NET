using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebRestApi.Controllers
{
    ///
    [Authorize(Roles = "user")]
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
                var user = await _dataService.GetUserByIdAsync(id);
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
    }
}

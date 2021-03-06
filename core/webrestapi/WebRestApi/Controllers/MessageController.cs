using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using WebRestApi.Service.Models.Client;

namespace WebRestApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private ILogger _logger;
        private IDataService _dataService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dataService"></param>
        public MessageController(ILogger logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        /// <summary>
        /// Get all messages from db
        /// </summary>
        /// <returns>All existing messages</returns>
        /// <response code="200">If operation has been completed without any exception</response>
        /// <response code="500">If something wrong had happen during getting users</response>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var messages = await _dataService.GetAllMessagesAsync();
                return Ok(messages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Something went wrong" });
            }
        }

        /// <summary>
        /// Get Message by Id
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id">Message Id</param>
        /// <returns>Message</returns>
        /// <response code="200">If operation has been completed without any exception</response>
        /// <response code="500">If something wrong had happen during gettting the user</response>
        [Authorize(Roles = "admin")]
        [HttpGet("{id}", Name = "GetMessageById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetMessageByUser, "Get messages by user");

            try
            {
                var messages = await _dataService.GetMessageById(id);
                return Ok(messages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Something went wrong" });
            }
        }

        /// <summary>
        /// Get all messages sent by User
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id">User Id</param>
        /// <returns>Collection of messages</returns>
        /// <response code="200">If operation has been completed without any exception</response>
        /// <response code="500">If something wrong had happen during gettting the user</response>
        [Authorize(Roles = "user, admin")]
        [HttpGet("user", Name = "GetMessagesByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUser([FromQuery] int id)
        {
            _logger.LogInformation(LoggingEvents.GetMessageByUser, "Get messages by user");

            try
            {
                var messages = await _dataService.GetAllMessagesByUserAsync(id);
                return Ok(messages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Something went wrong" });
            }
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="message"></param>
        /// <response code="200">If operation has been completed without any exception</response>
        /// <response code="400">If a sender or receiver could not be found</response>
        /// <response code="500">If something wrong had happen during getting users</response>
        [Authorize(Roles = "user, admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ClientMessage message)
        {
            var userFrom = await _dataService.GetUserByIdAsync(message.FromId);
            var userTo = await _dataService.GetUserByIdAsync(message.ToId);

            if (userFrom == null || userTo == null)
            {
                return BadRequest(new ErrorResponse { Message = "Should specify correct id for both sender and receiver" });
            }

            try
            {
                var result = await _dataService.SendMessageAsync(message);
                if (!result)
                {
                    return BadRequest(new ErrorResponse { Message = "Message could not be sent" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on sending new message.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                var error = new ErrorResponse { Message = "Error has accured on sending message" };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        /// <summary>
        /// Remove a message by Id
        /// </summary>
        /// <param name="id">Message Id</param>
        /// <response code="200">Message has been successfully removed</response>
        /// <response code="400">Message with the specified Id could not be found</response>
        /// <response code="500">Something went wrong</response>
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteUser, $"Delete Message with Id {id}");

            try
            {
                var message = await _dataService.DeleteMessageAsync(id);
                if (message == null)
                {
                    return BadRequest(new ErrorResponse { Message = $"Message with with the Id = {id} could not be found" });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnDeletingUser, $"Error on deleting Message with Id {id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                var error = new ErrorResponse { Message = "Error has accured on removing message" };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        /// <summary>
        /// Remove a message by Id
        /// </summary>
        /// <param name="id">Message Id</param>
        /// <response code="200">Message has been successfully removed</response>
        /// <response code="400">Message with the specified Id could not be found</response>
        /// <response code="500">Something went wrong</response>
        [Authorize(Roles = "user")]
        [HttpDelete("user", Name = "DeleteByUserId")]
        public async Task<IActionResult> DeleteByUser([FromBody] int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteUser, $"Delete Message with Id {id}");

            try
            {
                var message = await _dataService.DeleteMessageAsync(id);
                if (message == null)
                {
                    return BadRequest(new ErrorResponse { Message = $"Message with with the Id = {id} could not be found" });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnDeletingUser, $"Error on deleting Message with Id {id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                var error = new ErrorResponse { Message = "Error has accured on removing message" };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
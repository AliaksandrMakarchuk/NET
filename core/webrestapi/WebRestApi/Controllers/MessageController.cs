using System;
using System.Threading.Tasks;
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var messages = await _dataService.GetAllMessagesAsync();
            return Ok(messages);
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
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
                return BadRequest(new ErrorResponse { Message = $"Should specify both From and To Ids" });
            }

            // try
            // {
            var result = await _dataService.SendMessageAsync(message);
            return Ok();
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on sending new message.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
            //     var error = new ErrorResponse { Message = "Error has accured on sending message" };
            //     return StatusCode(StatusCodes.Status500InternalServerError, error);
            // }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteUser, $"Delete Message with Id {id}");

            try
            {
                await _dataService.DeleteMessageAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnDeletingUser, $"Error on deleting Message with Id {id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return BadRequest(new ErrorResponse { Message = "Error on removing specified Message" });
            }

            return Ok();
        }
    }
}
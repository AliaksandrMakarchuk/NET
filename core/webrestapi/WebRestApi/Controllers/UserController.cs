using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebRestApi.Service;
using WebRestApi.Service.Models;
using Microsoft.AspNetCore.Authorization;
using WebRestApi.Service.Models.Client;
using Microsoft.EntityFrameworkCore;

namespace WebRestApi.Controllers
{
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger _logger;
        private IDataService _dataService;

        ///
        public UserController(
            ILogger<UserController> logger,
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "user, admin")]
        [HttpGet("{id}", Name = "GetUserById")]
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
        public async Task<IActionResult> Post([FromBody] ClientUser user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                return BadRequest(new ErrorResponse { Message = "User first name and last name should be filled in" });
            }

            try
            {
                var newUser = await _dataService.CreateNewUserAsync(user);
                return Created("here", newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on creating new User.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                var error = new ErrorResponse { Message = "Error has accured on saving changes" };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

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
        public async Task<IActionResult> Put([FromBody] ClientUser user)
        {
            _logger.LogInformation(LoggingEvents.GetUserById, $"Update User with Id {user.Id}");

            try
            {
                // var existingUser = await _dataService.GetUserById(user.Id);

                // if (existingUser == null)
                // {
                //     return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
                // }

                // var updatedUser = await _dataService.UpdateUser(existingUser);
                // return Ok(updatedUser);

                var updatedUser = await _dataService.UpdateUserAsync(user);

                if (updatedUser == null)
                {
                    return BadRequest(new ErrorResponse { Message = "User with specified identifier could not be found" });
                }

                return Ok(updatedUser);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnSavingChanges, $"Error on authorizing User with Id {user.Id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Error has accured on saving changes" });
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <remarks></remarks>
        /// <response code="200">If User has heen successfully deleted</response>
        /// <response code="400">If could not find User by id</response>
        /// <response code="500">If something went wrong during removing the User</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteUser, $"Delete User with Id {id}");

            try
            {
                var user = await _dataService.DeleteUserAsync(id);
                if (user == null)
                {
                    return BadRequest(new ErrorResponse { Message = $"Could not find a user by id: {id}" });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ErrorOnDeletingUser, $"Error on deleting User with Id {id}.{Environment.NewLine}Exception message: {ex.Message}{Environment.NewLine}Exception StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Error on removing specified User" });
            }
        }
    }
}

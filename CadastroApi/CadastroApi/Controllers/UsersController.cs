using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace CadastroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        private readonly RegistrationContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IConfiguration configuration, RegistrationContext context, ILogger<UsersController> logger)
        {
            Configuration = configuration;
            _logger = logger;
            _context = context;
        }

            // GET: api/Users
            [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var allUsers = await _context.Users.ToListAsync();
                _logger.LogInformation("Succefuly GetUser() with this properties :");
                return allUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error at GetAllUsers() with this properties :");
                return null;
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {

            try
            {
                var user = await _context.Users.FindAsync(id);
                _logger.LogInformation("Succefuly GetUser() with this properties :");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error at GetUser() with this properties :");
                return NotFound();
            }

        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser( User user)
        {

            try
            {
                try{
                    _context.Entry(user).State = EntityState.Modified;
                }
                catch { 
                    return BadRequest();
                }
                await _context.SaveChangesAsync();
                _logger.LogInformation("Succefuly PutUser() with this properties :");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(user.Id))
                {
                    _logger.LogError(ex, "Not found the id at PutUser() with this properties :");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("Succefuly PutUser() with this properties :");
                    return NoContent();
                }
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if(user.FirstName == null)
            {
                _logger.LogError("Error at PostUser(), FirstName is null");
                return null;

            }
            else if(user.Age == 0)
            {
                _logger.LogError("Error at PostUser(), Age is null");
                return null;
            }
            user.CreationDate = DateTime.Now;
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Succefuly PostUser() with this properties :");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error at PostUser() with this properties :");
                return NotFound();
            }


            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if(user == null)
                {
                    _logger.LogError("Not found the id with this properties :");
                    return NotFound();
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Succefuly DeleteUser() with this properties :");
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error at DeleteUser() with this properties :");
                return NotFound();
            }
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

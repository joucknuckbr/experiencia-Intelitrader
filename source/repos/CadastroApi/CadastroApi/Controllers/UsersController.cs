using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
                _logger.LogInformation("Succefuly GetAllUsers() : (");
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error at GetAllUsers() : (");
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
                _logger.LogInformation("Succefuly GetUser() with id -> {id} : (", id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error at GetUser() with id -> {id} : (", id);
                return NotFound();
            }

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                _logger.LogError("Bad Request at PutUser() with id -> {id} : (", id);
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    _logger.LogError(ex, "Not found the id -> {id} at PutUser() : (", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation("Succefuly PutUser() with id -> {id} : (", id);
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Succefuly PostUser() with id -> {Id} : (", user.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error at PostUser() : (");
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
                    _logger.LogError("Not found the id -> {id} at DeleteUser() : (", id);
                    return NotFound();
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Succefuly DeleteUser() with id -> {Id} : (", user.Id);
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error at DeleteUser() with id -> {id} : (", id);
                return NotFound();
            }
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

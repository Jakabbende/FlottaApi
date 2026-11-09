using FlottaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FlottaContext _context;
        public UserController(FlottaContext context)
        {
            _context = context;
        }


        [HttpGet("GetAllUser")]
        public IActionResult GetAllUser()
        {
            try
            {
                List<User> user = _context.Users.ToList();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("PostUser")]
        public IActionResult PostUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                User us = _context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (us == null)
                {
                    return NotFound("Hianyzó user");
                }
                _context.ChangeTracker.Clear();
                _context.Users.Update(us);
                _context.SaveChanges();
                return Ok("User frissitve");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                User userdel = _context.Users.FirstOrDefault(u => u.Id == id);
                if (userdel != null)
                {
                    _context.ChangeTracker.Clear();
                    _context.Users.Remove(userdel);
                    _context.SaveChanges();
                    return Ok("Sikeres törlés");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

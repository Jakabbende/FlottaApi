using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlottaApi.Models;

namespace FlottaApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SoforController : ControllerBase
    {
        private FlottaContext _context;
        public SoforController(FlottaContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllSoforok")]
        public IActionResult GetAllsoforok()
        {
            try
            {
                List<Sofor> soforok = _context.Sofors.ToList();
                return Ok(soforok);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetSoforById/{id}")]
        public IActionResult GetSoforById(int id) {             
            try
            {
                Sofor sofor = _context.Sofors.FirstOrDefault(s => s.Id == id);
                if (sofor == null)
                {
                    return NotFound();
                }
                return Ok(sofor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("PostSofor")]
        public IActionResult PostSofor(Sofor sofor)
        {
            try
            {
                _context.Sofors.Add(sofor);
                _context.SaveChanges();
                return Ok("Sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateSofor")]
        public IActionResult UpdateSofor(Sofor sofor)
        {
            try
            {
                if(sofor == null)
                {
                    return BadRequest();
                }

                Sofor so = _context.Sofors.FirstOrDefault(s => s.Id == sofor.Id);
                if(so == null)
                {
                    return NotFound("Hianyzó sofor");
                }
                _context.ChangeTracker.Clear();
                _context.Sofors.Update(so);
                _context.SaveChanges();
                return Ok("Sofor frissitve");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteSofor/{id}")]
        public IActionResult DeleteSofor(int id)
        {
            try
            {
                Sofor sofordel = _context.Sofors.FirstOrDefault(s => s.Id == id);
                if(sofordel != null)
                {
                    _context.ChangeTracker.Clear();
                    _context.Sofors.Remove(sofordel);
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

        //[HttpGet("GetGepjarmu")]
        //public IActionResult GetAllgepjarmu()
        //{
        //    try
        //    {
        //        List<Gepjarmu> gepj = _context.Gepjarmus.ToList();
        //        return Ok(gepj);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}

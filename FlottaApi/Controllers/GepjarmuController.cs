using FlottaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GepjarmuController : ControllerBase
    {
        private FlottaContext _context;
        public GepjarmuController(FlottaContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllGepjarmu")]
        public IActionResult GetAll()
        {
            try
            {
                List<Gepjarmu> gepjarmus = new List<Gepjarmu>();
                gepjarmus = _context.Gepjarmus.ToList();
                if (gepjarmus.Count == 0)
                {
                    return NotFound("Nem található eredmeny");
                }
                else
                {
                    return Ok(gepjarmus);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetByRendszam/{rendszam}")]
        public IActionResult GetByrendsz(string rendszam)
        {
            try
            {
                var res = _context.Gepjarmus.FirstOrDefault(g => g.Rendszam == rendszam);
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("UjGepjarmu")]
        public IActionResult PostGepjarmu(Gepjarmu gepjarmu)
        {
            if (gepjarmu == null)
            {
                return BadRequest("Hiányzó adatok");
            }
            try
            {
                _context.Gepjarmus.Add(gepjarmu);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetJarmuByIdSoforok/{id}")]
        public IActionResult GetByGepjarmuidSoforid(int id)
        {
            try
            {
                if(_context.Gepjarmus.FirstOrDefault(g => g.Id == id) == null){
                    return NotFound("Hiányzó gépjarmű");
                }
                List<Sofor> result = _context.Kikuldottjarmus.Include(k => k.SoforNavigation)
                .Include(k => k.Gepjarmu).Where(k => k.GepjarmuId == id)
                .Select(k => k.SoforNavigation).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        [HttpDelete("JarmuTorlese/{id}")]
        public IActionResult DeleteGepjarmu (int id)
        {
            try
            {
                Gepjarmu gepj = _context.Gepjarmus.FirstOrDefault(g => g.Id == id);
                if (gepj == null)
                {
                    return NotFound("Hiányzó gépjármű");
                }
                _context.ChangeTracker.Clear();
                _context.Gepjarmus.Remove(new Gepjarmu { Id = id});
                _context.SaveChanges();
                return Ok();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

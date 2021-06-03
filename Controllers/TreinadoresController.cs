using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BelezinhaFut.Models;

namespace BelezinhaFut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinadoresController : ControllerBase
    {
        private readonly BelezinhaContext _context;

        public TreinadoresController(BelezinhaContext context)
        {
            _context = context;
        }

        // GET: api/Treinadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Treinador>>> GetTreinadores()
        {
            return await _context.Treinadores.ToListAsync();
        }

        // GET: api/Treinadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Treinador>> GetTreinador(long id)
        {
            var treinador = await _context.Treinadores.FindAsync(id);

            if (treinador == null)
            {
                return NotFound();
            }

            return treinador;
        }

        // PUT: api/Treinadores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreinador(long id, Treinador treinador)
        {
            if (id != treinador.Id)
            {
                return BadRequest();
            }

            _context.Entry(treinador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Treinadores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Treinador>> PostTreinador(Treinador treinador)
        {
            _context.Treinadores.Add(treinador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TreinadorExists(treinador.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTreinador", new { id = treinador.Id }, treinador);
        }

        // DELETE: api/Treinadores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Treinador>> DeleteTreinador(long id)
        {
            var treinador = await _context.Treinadores.FindAsync(id);
            if (treinador == null)
            {
                return NotFound();
            }

            _context.Treinadores.Remove(treinador);
            await _context.SaveChangesAsync();

            return treinador;
        }

        private bool TreinadorExists(long id)
        {
            return _context.Treinadores.Any(e => e.Id == id);
        }
    }
}

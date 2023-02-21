﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdecanesV2.Data;
using EdecanesV2.Models;
using EdecanesV2.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace EdecanesV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TandasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TandasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tandas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tanda>>> GetTandas()
        {
            return await _context.Tandas.ToListAsync();
        }

        // GET: api/Tandas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tanda>> GetTanda(int id)
        {
            var tanda = await _context.Tandas.FindAsync(id);

            if (tanda == null)
            {
                return NotFound();
            }

            return tanda;
        }

        // PUT: api/Tandas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTanda(int id, Tanda tanda)
        {
            if (id != tanda.Id)
            {
                return BadRequest();
            }

            _context.Entry(tanda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TandaExists(id))
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

        // POST: api/Tandas
        [HttpPost]
        public async Task<ActionResult<Tanda>> PostTanda(Tanda tanda)
        {
            _context.Tandas.Add(tanda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTanda", new { id = tanda.Id }, tanda);
        }

        // DELETE: api/Tandas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTanda(int id)
        {
            var tanda = await _context.Tandas.FindAsync(id);
            if (tanda == null)
            {
                return NotFound();
            }

            _context.Tandas.Remove(tanda);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        // POST: api/Tandas/5
        [HttpPost("restoredeleted/{id}")]
        public IActionResult RestoreDeleted(int id)
        {
            try
            {
                _context.Tandas.RestoreDeleted(id);
                _context.SaveChanges();
                return Ok("Restored");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Tandas/5
        [HttpGet("deleted")]
        public IActionResult Deleted()
        {
            return Ok(_context.Tandas.IgnoreQueryFilters().Where(t => t.DeletedAt.HasValue).ToList());
        }

        // GET: api/Tandas/deleted/5
        [HttpGet("deleted/{id}")]
        public IActionResult Deleted(int id)
        {
            var tanda =  _context.Tandas.IgnoreQueryFilters().FirstOrDefault(t => t.Id == id);

            if (tanda == null)
            {
                return NotFound();
            }

            return Ok(tanda);
        }

        private bool TandaExists(int id)
        {
            return _context.Tandas.Any(e => e.Id == id);
        }
    }
}

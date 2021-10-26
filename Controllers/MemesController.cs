using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIGenerator.DTOs;
using AutoMapper;
using APIGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using APIGenerator.ActionFilters;

namespace APIGenerator.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class MemesController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;

        public MemesController(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Memes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemesImages>>> GetImageDTO()
        {

            return await _context.MemesImages.ToListAsync();
        }

        // GET: api/Memes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDTO>> GetImageDTO(int id)
        {
            var imageDTO = await _context.MemesImages.FindAsync(id);
            var ImageDTO = new ImageDTO();

            if (imageDTO == null)
            {
                return NotFound();
            }

            return ImageDTO;
        }

        // PUT: api/Memes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageDTO(int id, ImageDTO imageDTO)
        {
            if (id != imageDTO.Id)
            {
                return BadRequest();
            }

           // _context.Entry(imageDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageDTOExists(id))
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

        // POST: api/Memes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ImageDTO>> PostImageDTO([FromForm] ImageDTO imageDTO)
        {
            var meme = _mapper.Map<MemesImages>(imageDTO);

            _context.MemesImages.Add(meme);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageDTO", new { id = imageDTO.Id }, imageDTO);
        }

        // DELETE: api/Memes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ImageDTO>> DeleteImageDTO(int id)
        {
            var imageDTO = await _context.MemesImages.FindAsync(id);
            if (imageDTO == null)
            {
                return NotFound();
            }

            _context.MemesImages.Remove(imageDTO);
            await _context.SaveChangesAsync();

            return new ImageDTO();
        }

        private bool ImageDTOExists(int id)
        {
            return _context.MemesImages.Any(e => e.Id == id);
        }
    }
}

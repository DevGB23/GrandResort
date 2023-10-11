using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resort_Web.Data;
using Resort_Web.Models;
using Resort_Web.Models.DTOs;

namespace Resort_Web.Controllers
{
    [Route("/api/VillaAPI")]
    // [ApiController]
    public class VillaAPIController : ControllerBase
    {

        // private readonly ILogging _logger;
        private readonly ApplicationDbContext _db = null!;
        public VillaAPIController (ApplicationDbContext db)
        {
            // _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            return Ok(await _db.Villas.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if ( villa is null )
            {
               return NotFound();
            }

            return Ok(villa);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if ( villa is null )
            {
               return NotFound();
            }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDTO)
        {
            if ( villaDTO == null ||  id != villaDTO.Id)
            {
                return BadRequest();
            }

            if (id == 0)
            {
                return BadRequest();
            }

            Villa villaModel = new (){
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Rate = villaDTO.Rate,
                Occupancy = villaDTO.Occupancy,
                SqFt = villaDTO.SqFt
            };
            
            if ( villaModel is null )
            {
               return NotFound();
            }

            _db.Villas.Update(villaModel);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "PartialUpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PartialUpdateVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if ( patchDTO == null ||  id == 0)
            {
                return BadRequest();
            }

            if (id == 0)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if ( villa is null )
            {
               return NotFound();
            }

            VillaUpdateDTO villaDTOModel = new (){
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Rate = villa.Rate,
                Occupancy = villa.Occupancy,
                SqFt = villa.SqFt
            };

            patchDTO.ApplyTo(villaDTOModel, ModelState);

            Villa villaModel = new (){
                Amenity = villaDTOModel.Amenity,
                Details = villaDTOModel.Details,
                Id = villaDTOModel.Id,
                ImageUrl = villaDTOModel.ImageUrl,
                Name = villaDTOModel.Name,
                Rate = villaDTOModel.Rate,
                Occupancy = villaDTOModel.Occupancy,
                SqFt = villaDTOModel.SqFt
            };

            
            _db.Villas.Update(villaModel);
            await _db.SaveChangesAsync();

            if ( !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async  Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaDTO)
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            if (await _db.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == villaDTO.Name.ToLower()) is not null )
            {
                ModelState.AddModelError("CustomError", "Villa already exist!");
                return BadRequest(ModelState);
            }

            if ( villaDTO is null )
            {
                return BadRequest(villaDTO);
            }
            
            Villa model = new Villa (){
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,                
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Rate = villaDTO.Rate,
                Occupancy = villaDTO.Occupancy,
                SqFt = villaDTO.SqFt
            };
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }
    }
}

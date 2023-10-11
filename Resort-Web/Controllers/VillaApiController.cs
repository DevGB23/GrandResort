using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db = null!;
        public VillaAPIController (ApplicationDbContext db, IMapper mapper)
        {
            // _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
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

            return Ok(_mapper.Map<VillaDTO>(villa));
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaUpdateDTO updateDTO)
        {
            if ( updateDTO == null ||  id != updateDTO.Id)
            {
                return BadRequest();
            }

            if (id == 0)
            {
                return BadRequest();
            }

            Villa villaModel = _mapper.Map<Villa>(updateDTO);
            
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

            VillaUpdateDTO villaDTOModel = _mapper.Map<VillaUpdateDTO>(villa);

            if ( villa is null )
            {
               return NotFound();
            }

            patchDTO.ApplyTo(villaDTOModel, ModelState);

            Villa villaModel = _mapper.Map<Villa>(villaDTOModel);

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
        public async  Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO createDTO)
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            if (await _db.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == createDTO.Name.ToLower()) is not null )
            {
                ModelState.AddModelError("CustomError", "Villa already exist!");
                return BadRequest(ModelState);
            }

            if ( createDTO is null )
            {
                return BadRequest(createDTO);
            }
            
            Villa villaModel = _mapper.Map<Villa>(createDTO);

            // Villa model = new Villa (){
            //     Amenity = createDTO.Amenity,
            //     Details = createDTO.Details,                
            //     ImageUrl = createDTO.ImageUrl,
            //     Name = createDTO.Name,
            //     Rate = createDTO.Rate,
            //     Occupancy = createDTO.Occupancy,
            //     SqFt = createDTO.SqFt
            // };
            await _db.Villas.AddAsync(villaModel);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = villaModel.Id }, villaModel);
        }
    }
}

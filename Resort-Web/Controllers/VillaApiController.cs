using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Resort_Web.Models;
using Resort_Web.Models.DTOs;
using Resort_Web.Repository.IRepository;
using System.Net;
namespace Resort_Web.Controllers
{
    [Route("/api/VillaAPI")]
    // [ApiController]
    public class VillaAPIController : ControllerBase
    {

        // private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _repo = null!;
        protected APIResponse _response;
        public VillaAPIController (IVillaRepository repo, IMapper mapper)
        {
            // _logger = logger;
            _repo = repo;
            _mapper = mapper;
            this._response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try {
                IEnumerable<Villa> villaList = await _repo.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() {
                    ex.ToString()
                };
            
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _repo.GetAsync(v => v.Id == id);

                if ( villa is null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaModel = await _repo.GetAsync(v => v.Id == id);

                if ( villaModel is null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.RemoveAsync(villaModel);

                _response.StatusCode = HttpStatusCode.NoContent; 
                _response.IsSuccess = true;
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody]VillaUpdateDTO updateDTO)
        {
            try
            {
                if ( updateDTO == null ||  id != updateDTO.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Villa villaModel = _mapper.Map<Villa>(updateDTO);
                
                if ( villaModel is null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.UpdateAsync(villaModel);

                _response.StatusCode = HttpStatusCode.NoContent; 
                _response.IsSuccess = true;
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() {
                    ex.ToString()
                };
            }
            return _response;
        }


        [HttpPatch("{id:int}", Name = "PartialUpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PartialUpdateVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if ( patchDTO == null ||  id == 0)
            {
                return BadRequest();
            }

            if (id == 0)
            {
                return BadRequest();
            }

            
            var villa = await _repo.GetAsync(v => v.Id == id, tracked: false);

            VillaUpdateDTO villaDTOModel = _mapper.Map<VillaUpdateDTO>(villa);

            if ( villa is null )
            {
               return NotFound();
            }

            patchDTO.ApplyTo(villaDTOModel, ModelState);

            Villa villaModel = _mapper.Map<Villa>(villaDTOModel);

            await _repo.UpdateAsync(villaModel);
            
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
        public async  Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO createDTO)
        {
            try
            {
                if ( !ModelState.IsValid )
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);                    
                }

                if (await _repo.GetAsync(v => v.Name.ToLower() == createDTO.Name.ToLower()) is not null )
                {
                    ModelState.AddModelError("CustomError", "Villa already exist!");
                    return BadRequest(ModelState);
                }

                if ( createDTO is null )
                {
                    return BadRequest(createDTO);
                }
                
                Villa villaModel = _mapper.Map<Villa>(createDTO);
                
                await _repo.CreateAsync(villaModel);

                _response.Result = _mapper.Map<VillaDTO>(villaModel);
                _response.StatusCode = HttpStatusCode.Created;            
                
                return CreatedAtRoute("GetVilla", new { id = villaModel.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() {
                    ex.ToString()
                };
            }
            return _response;
        }
    }
}


using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Resort_Web.Models;
using Resort_Web.Models.DTOs;
using Resort_Web.Repository.IRepository;

namespace Resort_Web.Controllers
{
    [Route("/api/VillaNumberAPI")]
    public class VillaNumberController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _repo = null!;
        protected APIResponse _response;
        public VillaNumberController (IVillaNumberRepository repo, IMapper mapper)
        {
            // _logger = logger;
            _repo = repo;
            _mapper = mapper;
            this._response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try {
                IEnumerable<VillaNumber> villaNumberList = await _repo.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
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

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _repo.GetAsync(v => v.VillaNo == id);

                if ( villaNumber is null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumberModel = await _repo.GetAsync(v => v.VillaNo == id);

                if ( villaNumberModel is null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.RemoveAsync(villaNumberModel);

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

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody]VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if ( updateDTO == null ||  id != updateDTO.VillaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                VillaNumber villaNumberModel = _mapper.Map<VillaNumber>(updateDTO);
                
                if ( villaNumberModel is null )
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _repo.UpdateAsync(villaNumberModel);

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


        [HttpPatch("{id:int}", Name = "PartialUpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PartialUpdateVillaNumber(int id, [FromBody] JsonPatchDocument<VillaNumberUpdateDTO> patchDTO)
        {
            if ( patchDTO == null ||  id == 0)
            {
                return BadRequest();
            }

            if (id == 0)
            {
                return BadRequest();
            }

            
            var villa = await _repo.GetAsync(v => v.VillaNo == id, tracked: false);

            VillaNumberUpdateDTO villaNumberDTOModel = _mapper.Map<VillaNumberUpdateDTO>(villa);

            if ( villa is null )
            {
               return NotFound();
            }

            patchDTO.ApplyTo(villaNumberDTOModel, ModelState);

            VillaNumber villaNumberModel = _mapper.Map<VillaNumber>(villaNumberDTOModel);

            await _repo.UpdateAsync(villaNumberModel);
            
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
        public async  Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if ( !ModelState.IsValid )
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);                    
                }

                if (await _repo.GetAsync(v => v.VillaNo == createDTO.VillaNo) is not null )
                {
                    ModelState.AddModelError("CustomError", "Villa Number already exist!");
                    return BadRequest(ModelState);
                }

                if ( createDTO is null )
                {
                    return BadRequest(createDTO);
                }
                
                VillaNumber villaNumberModel = _mapper.Map<VillaNumber>(createDTO);
                
                await _repo.CreateAsync(villaNumberModel);

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumberModel);
                _response.StatusCode = HttpStatusCode.Created;            
                
                return CreatedAtRoute("GetVillaNumber", new { id = villaNumberModel.VillaNo }, _response);
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
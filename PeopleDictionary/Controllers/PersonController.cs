using Microsoft.AspNetCore.Mvc;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Api.Models.Responses;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;

namespace PeopleDictionary.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult> GetByIdAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _personService.GetByIdAsync(id);

                var response = GetByIdResponse.BuildFrom(result);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/quick")]
        public async Task<ActionResult> QuickSearchAsync([FromQuery] string? name, [FromQuery] string? lastname, [FromQuery] string? personalId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _personService.QuickSearchAsync(name, lastname, personalId);

                if (result == null)
                {
                    return BadRequest();
                }

                var response = new List<GetByIdResponse>();


                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreatePersonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var model = CreatePersonRequest.BuildFrom(request);

                await _personService.CreateAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdatePersonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _personService.UpdateAsync(request.Id, request.Name, request.Lastname, request.Gender, request.PersonalId, request.DateOfBirth, request.CityId, request.TelNumbers);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("image")]
        public async Task<ActionResult> UploadOrUpdateImageAsync([FromRoute] int id, [FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _personService.UploadOrUpdateImageAsync(id, file);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("relation/add")]
        public async Task<ActionResult> AddRelationAsync([FromQuery] int personId, [FromQuery] int relatedId, [FromQuery] RelationEnums type)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _personService.AddRelationAsync(personId, relatedId, type);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("relation/remove")]
        public async Task<ActionResult> RemoveRelationAsync([FromQuery] int personId, [FromQuery] int relatedId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _personService.RemoveRelationAsync(personId, relatedId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveRelationAsync([FromQuery] int personId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _personService.Remove(personId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

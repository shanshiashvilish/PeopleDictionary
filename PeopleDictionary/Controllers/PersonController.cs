using Microsoft.AspNetCore.Mvc;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Api.Models.Responses;
using PeopleDictionary.Api.Models.Validations;
using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<BaseModel<GetPersonResponse?>>> GetByIdAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                if (id < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, "Id must be more than 1"));
                }

                var result = await _personService.GetByIdAsync(id);

                if (result == null || !result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return await GetPersonResponse.BuildFrom(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/quick")]
        public async Task<ActionResult<BaseModel<List<GetPersonResponse>>>>? QuickSearchAsync([FromQuery] QuickSearchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                // TO DO: validation

                var result = await _personService.QuickSearchAsync(request.Name, request.Lastname, request.PersonalId);

                if (result == null || !result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return await GetPersonResponse.BuildFromList(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseModel<bool>>> CreateAsync([FromBody] CreatePersonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var validator = new CreatePersonValidationModel(request);
                var validate = await validator.ValidateAsync(request);

                if (!validate.IsValid)
                {
                    return BadRequest(new BaseModel<bool>(false, default, string.Join(",\n", validate.Errors.Select(e => e.ErrorMessage))));
                }

                var model = CreatePersonRequest.BuildFrom(request);

                return await _personService.CreateAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseModel<bool>>> UpdateAsync([FromBody] UpdatePersonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                // TO DO: validations

                return await _personService.UpdateAsync(request.Id, request.Name, request.Lastname, request.Gender, request.PersonalId, request.DateOfBirth, request.CityId, request.TelNumbers);
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
        public async Task<ActionResult<BaseModel<bool>>> AddRelationAsync([FromQuery] int personId, [FromQuery] int relatedId, [FromQuery] RelationEnums type)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                // TO DO: validations
                if (personId < 1 || relatedId < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, "Id must be more than 1"));
                }

                return await _personService.AddRelationAsync(personId, relatedId, type);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("relation/remove")]
        public async Task<ActionResult<BaseModel<bool>>> RemoveRelationAsync([FromQuery] int personId, [FromQuery] int relatedId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                // TO DO: validations
                if (personId < 1 || relatedId < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, "Id must be more than 1"));
                }

                return await _personService.RemoveRelationAsync(personId, relatedId);
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
                if (personId < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, "Id must be more than 1"));
                }

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

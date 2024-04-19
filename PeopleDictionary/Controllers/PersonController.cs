using Microsoft.AspNetCore.Mvc;
using PeopleDictionary.Api.Models.Requests;
using PeopleDictionary.Api.Models.Responses;
using PeopleDictionary.Api.Models.Validations;
using PeopleDictionary.Core.Base;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using PeopleDictionary.Core.Resources;

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

        [HttpGet]
        public async Task<ActionResult<BaseModel<GetPersonResponse?>>> GetByIdAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                if (id < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, RsValidation.IdMinValue));
                }

                var result = await _personService.GetByIdAsync(id);

                if (result == null || !result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return Ok(await GetPersonResponse.BuildFrom(result));
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
                var validator = new UpdatePersonValidationModel(request);
                var validate = await validator.ValidateAsync(request);

                if (!validate.IsValid)
                {
                    return BadRequest(new BaseModel<bool>(false, default, string.Join(",\n", validate.Errors.Select(e => e.ErrorMessage))));
                }

                var result = await _personService.UpdateAsync(request.Id, request.Name, request.Lastname, request.Gender, request.PersonalId, request.DateOfBirth, request.CityId, request.TelNumbers);

                if (!result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return Ok(new BaseModel<bool>(true, true, string.Empty));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult RemoveRelation([FromQuery] int personId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                if (personId < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, RsValidation.IdMinValue));
                }

                _personService.Remove(personId);

                return Ok(new BaseModel<bool>(true, true, string.Empty));
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
                var validator = new QuickSearchValidationModel(request);
                var validate = await validator.ValidateAsync(request);

                if (!validate.IsValid)
                {
                    return BadRequest(new BaseModel<GetPersonResponse?>(false, default, string.Join(",\n", validate.Errors.Select(e => e.ErrorMessage))));
                }

                var result = await _personService.QuickSearchAsync(request.Name, request.Lastname, request.PersonalId);

                if (result == null || !result.IsSuccess)
                {
                    return BadRequest(new BaseModel<GetPersonResponse?>(false, default, result?.Message));
                }

                return Ok(await GetPersonResponse.BuildFromList(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("image")]
        public async Task<ActionResult<BaseModel<bool>>> UploadOrUpdateImageAsync([FromRoute] int id, [FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                if (id < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, RsValidation.IdMinValue));
                }

                var result = await _personService.UploadOrUpdateImageAsync(id, file);

                if(!result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return Ok(new BaseModel<bool>(true, true, string.Empty));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("relation/add")]
        public async Task<ActionResult<BaseModel<bool>>> AddRelationAsync([FromRoute] int personId, [FromQuery] int relatedId, [FromQuery] RelationEnums type)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                if (personId < 1 || relatedId < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, RsValidation.IdMinValue));
                }

                var result = await _personService.AddRelationAsync(personId, relatedId, type);

                if (!result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return Ok(new BaseModel<bool>(true, true, string.Empty));
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
                if (personId < 1 || relatedId < 1)
                {
                    return BadRequest(new BaseModel<bool>(false, default, RsValidation.IdMinValue));
                }

                var result = await _personService.RemoveRelationAsync(personId, relatedId);

                if (!result.IsSuccess)
                {
                    return BadRequest(new BaseModel<bool>(false, default, result?.Message));
                }

                return Ok(new BaseModel<bool>(true, true, string.Empty));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

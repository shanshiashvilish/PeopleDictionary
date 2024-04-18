using Microsoft.AspNetCore.Mvc;
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

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> QuickSearchAsync([FromQuery] string name, [FromQuery] string lastname, [FromQuery] string personalId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _personService.QuickSearchAsync(name, lastname, personalId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _personService.CreateAsync(person);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] int id, string name, string lastname, GenderEnums gender, string personalId, DateTime DateOfBirth, string city, List<TelephoneNumbers> telNumbers)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _personService.UpdateAsync(id, name, lastname, gender, personalId, DateOfBirth, city, telNumbers);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
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

        [HttpPatch]
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

        [HttpPatch]
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

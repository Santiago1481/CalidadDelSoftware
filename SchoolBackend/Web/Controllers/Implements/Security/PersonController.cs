using Business.Interfaces.Commands;
using Business.Interfaces.Querys;
using Entity.Dtos.Security.Person;
using Entity.Model.Security;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Implements.Abstract;

namespace Web.Controllers.Implements.Security
{
    public class PersonController
       : GenericController<
       Person,
       PersonQueryDto,
       PersonDto>
    {

        protected readonly IQueryPersonServices _servicesQuery;
        protected readonly ICommandPersonServices _servicesCommand;


        public PersonController(
            IQueryPersonServices q,
            ICommandPersonServices c)
          : base(q, c)
        {
            _servicesQuery = q;
            _servicesCommand = c;
        }

        [HttpGet("data/{personId}")]
        public async Task<IActionResult> GetUserRolsById(int personId)
        {
            var result = await _servicesQuery.GetDataCompleteServices(personId);
            return Ok(result);
        }


        [HttpPost("CreateComplet")]
        public virtual async Task<IActionResult> Create([FromBody][CustomizeValidator(RuleSet = "Full")] PersonCompleteDto dto)
        {
            var created = await _servicesCommand.CreateRemastered(dto);
            return Ok(created);

        }

        [HttpGet("PersonBasic/{personId}")]
        public async Task<IActionResult> GetPersonBasic(int personId)
        {
            var result = await _servicesQuery.GetPersonDataBasic(personId);
            return Ok(result);
        }

        // PUT: api/persons/5
        [HttpPut("UpdateComplete/{personId:int}")]
        public async Task<IActionResult> UpdatePerson(int personId, [FromBody] PersonCompleteDto dto)
        {
            if (dto == null)
                return BadRequest("El cuerpo de la petición es requerido.");

            var result = await _servicesCommand.UpdateRemasteredAsync(personId, dto);

            return Ok(result);
        }
    }

}

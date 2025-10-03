using AutoMapper;
using Business.Implements.Bases;
using Business.Interfaces.Querys;
using Data.Interfaces.Group.Commands;
using Entity.Dtos.Security.Person;
using Entity.Dtos.Security.User;
using Entity.Model.Business;
using Entity.Model.Security;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using Utilities.Helpers.Validations;

namespace Business.Implements.Commands.Security
{
    public class PersonCommandBusines : BaseCommandsBusiness<Person, PersonDto>, ICommandPersonServices
    {
        protected readonly ICommanPerson _data;
        public PersonCommandBusines(
            ICommanPerson data,
            IMapper mapper,
            ILogger<PersonCommandBusines> _logger,
            IGenericHerlpers helpers
            ) : base(data, mapper, _logger, helpers)
        {
            _data = data;

        }

        /// <summary>
        /// Valida un DTO utilizando las reglas de validación de FluentValidation
        /// </remarks>
        protected async Task EnsureValid(PersonDto dto)
        {
            var validationResult = await _helpers.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors);
                throw new ArgumentException($"Validación fallida: {errors}");
            }
        }

        protected async Task EnsureValid(PersonCompleteDto dto)
        {
            var validationResult = await _helpers.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors);
                throw new ArgumentException($"Validación fallida: {errors}");
            }
        }


        public virtual async Task<PersonCompleteDto> CreateRemastered(PersonCompleteDto current)
        {
            try
            {
                await EnsureValid(current);
                var entity = _mapper.Map<Person>(current);
                entity = await _data.InsertComplete(entity);
                _logger.LogInformation($"Creando nuevo {typeof(Person).Name}");
                return _mapper.Map<PersonCompleteDto>(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<PersonCompleteDto> UpdateRemasteredAsync(int id, PersonCompleteDto dto)
        {
            await EnsureValid(dto); // usa tu RuleSet de update

            var existing = await _data.QueryByIdTrackedAsync(id);
            if (existing is null)
                throw new NotFoundException($"No existe {nameof(Person)} con id {id}");

            // Mapear ENCIMA de la entidad cargada (tracked).
            // Gracias al Profile, los nulls no pisan valores.
            _mapper.Map(dto, existing);

            // Si viene DataBasic en el DTO y el existente no tenía, crear placeholder
            if (dto.DataBasic != null && existing.DataBasic == null)
            {
                existing.DataBasic = new DataBasic
                {
                    Person = existing   // EF fijará PersonId
                };
                // Vuelve a mapear solo DataBasic para aplicar campos no nulos
                _mapper.Map(dto.DataBasic, existing.DataBasic);
            }

            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _data.UpdateCompleteAsync(existing); // guarda en Data
            return _mapper.Map<PersonCompleteDto>(updated);
        }



    }
}

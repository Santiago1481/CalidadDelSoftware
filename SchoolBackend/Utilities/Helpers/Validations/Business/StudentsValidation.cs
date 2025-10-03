using Entity.Dtos.Business.Student;
using FluentValidation;

namespace Utilities.Helpers.Validations.Business
{
    public class StudentsValidation : AbstractValidator<StudentDto>
    {
        public StudentsValidation()
        {
            RuleSet("Full", () =>
            {
                RuleFor(x => x.GroupId)
                  .GreaterThan(0)
                    .WithMessage("El id de grupo no es valido.")
                    .NotEmpty().WithMessage("El id de grupo es obligatorio");

            });

            // Reglas para PATCH: solo valida si el campo viene presente
            RuleSet("Patch", () =>
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("El Id es obligatorio");

            });
        }


    }
    
}

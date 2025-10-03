using FluentValidation;


namespace Utilities.Helpers.Validations.ValidationsGenerics
{
    public static class CommonRules
    {
        //patrones de validaciones
        private const string NamePattern = @"^[\p{L}\s'\-]+$";

        // OJO: IRuleBuilderInitial en el "this"
        public static IRuleBuilderOptions<T, string?> StandardName<T>(
            this IRuleBuilderInitial<T, string?> rule,
            int min = 4, int max = 30)
        {
            return rule
                .Cascade(CascadeMode.Stop)
                .Must(s => !string.IsNullOrWhiteSpace(s))
                    .WithMessage("El nombre es obligatorio.")
                .Must(s => {
                    var t = s!.Trim();
                    return t.Length >= min && t.Length <= max;
                })
                    .WithMessage($"El nombre debe tener entre {min} y {max} caracteres.")
                .Matches(NamePattern)
                    .WithMessage("El nombre solo puede contener letras y espacios (sin números).");
        }

        public static IRuleBuilderOptions<T, string?> RequiredText<T>(
            this IRuleBuilderInitial<T, string?> rule)
        {
            return rule
                .Cascade(CascadeMode.Stop)
                .Must(s => !string.IsNullOrWhiteSpace(s))
                    .WithMessage("La descripción es obligatoria.");
        }
    }
}

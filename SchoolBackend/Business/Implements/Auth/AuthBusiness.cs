using Data.Implements.Auth;
using Entity.Dtos.Security.Auth;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;


namespace Business.Implements.Auth
{
    public class AuthBusiness
    {
        protected readonly LoginData _data;
        protected readonly ILogger<AuthBusiness> _logger;

        public AuthBusiness(LoginData d, ILogger<AuthBusiness> logger)
        {
            _data = d;
            _logger = logger;
        }

        public async Task<Object> AuthApp(CredencialesDto login)
        {
            try
            {
                if (login == null)
                    throw new ExternalServiceException("Base de datos", $"acceso denegado:  {login.Email}");


                var updated = await _data.ValidarLoginAsync(login);

                if (updated == null)
                {
                    return new { message = "Acceso denegado, crendenciales incorrectas" };
                }


                return updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al dar acceso {login.Email}");
                throw new ExternalServiceException("Base de datos", $"Error acceso no autorizado {login.Email}", ex);
            }
        }

    }
}

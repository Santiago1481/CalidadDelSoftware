using Entity.Context.Main;
using Entity.Dtos.Security.Auth;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities.Jwt;

namespace Data.Implements.Auth
{
    public class LoginData
    {
        private AplicationDbContext _contenxt;
        private ILogger<LoginData> _logger;
        private readonly GenerateToken _jwt;

        public LoginData(AplicationDbContext context, ILogger<LoginData> logger, GenerateToken jwt)
        {
            _contenxt = context;
            _logger = logger;
            _jwt = jwt;
        }

        //<summary>
        // metodo que valida si el usuario existe en el sistema y asi poder validar sus credenciales
        //<summary>
        public async Task<AuthDto> ValidarLoginAsync(CredencialesDto credenciales)
        {
            //busqueda del usuario por nombre
            var user = await _contenxt.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == credenciales.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(credenciales.Password, user.Password))
            {
                var token = await _jwt.GeneradorToken(user.Id);

                return token;
            }

            return null;
        }

    }
}

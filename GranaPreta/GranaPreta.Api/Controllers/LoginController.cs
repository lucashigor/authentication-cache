using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using GranaPreta.Domain;
using GranaPreta.Api.Security;
using GranaPreta.Application.Interfaces;

namespace GranaPreta.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAppService _appService;

        public LoginController(IUserAppService appService)
        {
            _appService = appService;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]User usuario,
            [FromServices]SigningConfiguration signingConfigurations,
            [FromServices]TokenConfiguration tokenConfigurations)
        {
            bool credenciaisValidas = false;
            var usuarioBase = _appService.GetByEmail(usuario.Email);
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                credenciaisValidas = (usuarioBase != null && usuario.Email == usuarioBase.Email && usuario.AccessKey == usuarioBase.AccessKey);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Email, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                usuarioBase.Authenticated = true;
                usuarioBase.Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss");
                usuarioBase.Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss");
                usuarioBase.AccessToken = token;
                usuarioBase.Message = "OK";

                return usuarioBase;
            }
            else
            {
                return NotFound(
                    new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    }
                );
            }
        }
    }
}
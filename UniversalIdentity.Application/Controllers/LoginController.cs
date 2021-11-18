using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Text;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Domain.Models;
using UniversalIdentity.Domain.Wrappers;

namespace UniversalIdentity.Application.Controllers
{
    /// <summary>
    /// Gerencia os dados de login, como geração de Token
    /// </summary>
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILoginService _baseService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="baseService"></param>
        /// <param name="mapper"></param>
        public LoginController(ILoginService baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="login">Modelo para inserir</param>
        /// <returns>Id do obj</returns>
#if RELEASE
        [ApiExplorerSettings(IgnoreApi = true)]
#endif
        [AllowAnonymous]
        [SwaggerResponse(200, "Ok", typeof(Response<int>))]
        [SwaggerResponse(404, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(409, "Conflict", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] LoginCreateRequestModel login)
        {
            if (login == null)
                return BaseNotFound();

            if (_baseService.ExistsByEmail(login.Email))
            {
                return BaseConflict("E-mail já esta em uso.");
            }

            return Execute(() =>
            {
                var newLogin = _mapper.Map<Login>(login);
                newLogin.Pessoa.Status = true;

                newLogin.Pessoa.DataCadastro =
                newLogin.Pessoa.DataAtualizacao =
                newLogin.Pessoa.DataNascimento =
                newLogin.DataUltimoAcesso = System.DateTime.Now;

                return Response<int>.Create(_baseService.Add(newLogin).Id);
            });
        }

        /// <summary>
        /// Gera um token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse(200, "Ok", typeof(Response<TokenResponseModel>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        public IActionResult Login([FromBody] LoginPostRequestModel login)
        {
            if (login == null ||
                string.IsNullOrEmpty(login.Email) ||
                string.IsNullOrEmpty(login.Senha))
            {

                return BaseUnauthorized("Não autorizado", "Usuário ou senha inválidos.");
            }

            var loginResponse = _baseService.GetWithIncludesByEmailAndSenha(login.Email, login.Senha);
            if (loginResponse == null)
            {
                return BaseUnauthorized("Não autorizado", "Usuário ou senha inválidos.");
            }

            return Execute(() =>
            {
                var tokenRespnse = _baseService.GerarTokenJwt(loginResponse);
                return tokenRespnse;
            });
        }

        /// <summary>
        /// Retorna as Claims vinculadas no Token do Header
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(200, "Ok", typeof(Response<string>))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpGet("ObterDadosPorToken")]
        public IActionResult ObterDadosPorToken()
        {
            return Execute(() =>
            {
                var sb = new StringBuilder();
                foreach (var item in User.Claims)
                {
                    sb.AppendLine($"Tipo: {item.Type.Split('/').LastOrDefault()} / Valor: {item.Value}");
                }

                return Response<string>.Create(sb.ToString());
            });
        }

    }
}

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
        private readonly ILoginService _loginService;
        private readonly IUniversalIdentityService _universalIdentityService;
        private readonly IQRCodeService _qRCodeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="loginService"></param>
        /// <param name="universalIdentityService"></param>
        /// <param name="qRCodeService"></param>
        /// <param name="mapper"></param>
        public LoginController(ILoginService loginService,
            IUniversalIdentityService universalIdentityService,
            IQRCodeService qRCodeService,
            IMapper mapper)
        {
            _loginService = loginService;
            _universalIdentityService = universalIdentityService;
            _qRCodeService = qRCodeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="login">Modelo para inserir</param>
        /// <returns>Id do obj</returns>
#if RELEASE
        //[ApiExplorerSettings(IgnoreApi = true)]
#endif
        [AllowAnonymous]
        [SwaggerResponse(200, "Ok", typeof(Response<int>))]
        [SwaggerResponse(400, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(409, "Conflict", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] LoginCreateRequestModel login)
        {
            if (!ModelState.IsValid)
            {
                return BaseBadRequest("Requisição mal formatada", GetModelStateErros());
            }

            if (_loginService.ExistsByEmail(login.Email))
            {
                return BaseConflict("E-mail já esta em uso.");
            }

            return Execute(() =>
            {
                var newLogin = _mapper.Map<Login>(login);
                newLogin.Pessoa.Status = true;

                var dtNow = System.DateTime.Now;
                newLogin.Pessoa.DataCadastro = dtNow;
                newLogin.Pessoa.DataAtualizacao = dtNow;
                newLogin.DataUltimoAcesso = dtNow;

                newLogin.Pessoa.UniversalId = _universalIdentityService.GenerateUniversalIdentity();
                newLogin.Pessoa.UniversalIdBase64 = _qRCodeService.GenerateQRCode(newLogin.Pessoa.UniversalId);

                return Response<int>.Create(_loginService.Add(newLogin).Id);
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

            if (!ModelState.IsValid)
            {
                return BaseBadRequest("Requisição mal formatada", GetModelStateErros());
            }

            var loginResponse = _loginService.GetWithIncludesByEmailAndSenha(login.Email, login.Senha);
            if (loginResponse == null)
            {
                return BaseUnauthorized("Não autorizado", "Usuário ou senha inválidos.");
            }

            return Execute(() =>
            {
                var tokenRespnse = _loginService.GerarTokenJwt(loginResponse);
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

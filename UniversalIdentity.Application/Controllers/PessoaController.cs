using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalIdentity.Domain.Interfaces;
using UniversalIdentity.Domain.Models;
using UniversalIdentity.Domain.Wrappers;

namespace UniversalIdentity.Application.Controllers
{
    /// <summary>
    /// Controller para gerencia as pessoas
    /// </summary>
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : BaseController
    {
        private readonly ILoginService _baseService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="baseService"></param>
        ///  <param name="mapper"></param>
        public PessoaController(ILoginService baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }


        /// <summary>
        /// Retorna os dados do usuário autenticado
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(200, "Ok", typeof(Response<PessoaGetResponseModel>))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        [SwaggerResponse(404, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpGet()]
        public IActionResult Get()
        {
            var userId = GetCurrentUserId();

            if (userId <= 0)
                return BaseNotFound();

            return Execute(() =>
            {
                var login = _baseService.GetWithIncludesByUsuarioId(userId);
                var loginMapper = _mapper.Map<PessoaGetResponseModel>(login);
                return Response<PessoaGetResponseModel>.Create(loginMapper);
            });
        }
    }
}

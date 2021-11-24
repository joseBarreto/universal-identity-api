using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Filter;
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
        private readonly ILoginService _loginService;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="baseService"></param>
        /// <param name="pessoaService"></param>
        ///  <param name="mapper"></param>
        public PessoaController(ILoginService baseService, IPessoaService pessoaService, IMapper mapper)
        {
            _loginService = baseService;
            _pessoaService = pessoaService;
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
                var login = _loginService.GetWithIncludesByUsuarioId(userId);
                var loginMapper = _mapper.Map<PessoaGetResponseModel>(login);
                return Response<PessoaGetResponseModel>.Create(loginMapper);
            });
        }

        /// <summary>
        /// Retorna uma lista de registros
        /// O SearchTerm é pesquisado dentro do Nome, Documento e UniqueId
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [SwaggerResponse(200, "Ok", typeof(PagedResponse<IList<PessoaGetResponseModel>>))]
        [SwaggerResponse(400, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] PaginationFilterWithSearchTerm filter)
        {
            if (!ModelState.IsValid)
            {
                return BaseBadRequest("Requisição mal formatada", GetModelStateErros());
            }

            return Execute(() =>
            {
                if (string.IsNullOrEmpty(filter.SearchTerm))
                {
                    return CreatePagedReponse(new List<PessoaGetResponseModel>(), filter, 0);
                }

                var pessoas = _pessoaService.GetByTermWithIncludes(filter.SearchTerm, GetCurrentUserId(), filter.PageNumber, filter.PageSize, out int totalRecords);
                var pessoasModels = _mapper.Map<IList<Pessoa>, IList<PessoaGetResponseModel>>(pessoas);
                return CreatePagedReponse(pessoasModels, filter, totalRecords);
            });
        }
    }
}

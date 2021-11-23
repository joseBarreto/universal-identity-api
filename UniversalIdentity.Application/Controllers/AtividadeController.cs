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
    /// Gerencia os dados de atividades
    /// </summary>
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]
    public class AtividadeController : BaseController
    {
        private readonly IAtividadeService _atividadeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="atividadeService"></param>
        /// <param name="mapper"></param>
        public AtividadeController(IAtividadeService atividadeService, IMapper mapper)
        {
            _atividadeService = atividadeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Insere um novo registro de atividade para uma Pessoa especifica
        /// </summary>
        /// <param name="atividade">Modelo para inserir</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Ok", typeof(Response<int>))]
        [SwaggerResponse(400, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] AtividadeCreateRequestModel atividade)
        {
            if (!ModelState.IsValid || atividade.PessoaId == 0)
            {
                return BaseBadRequest("Requisição mal formatada", GetModelStateErros());
            }

            var autorId = GetCurrentUserId();

            if (atividade?.PessoaId == autorId)
            {
                return BaseConflict("Não é permitido adicionar uma atividade para si próprio.");

            }

            return Execute(() =>
            {
                var newAtividade = _mapper.Map<Atividade>(atividade);
                newAtividade.AutorId = autorId;
                newAtividade.DataCadastro = System.DateTime.Now;

                return Response<int>.Create(_atividadeService.Add(newAtividade).Id);
            });
        }

        /// <summary>
        /// Retorna uma atividade por id
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(200, "Ok", typeof(Response<AtividadeGetResponseModel>))]
        [SwaggerResponse(401, "Unauthorized", typeof(string))]
        [SwaggerResponse(404, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpGet()]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return BaseNotFound();

            return Execute(() =>
            {
                var atividade = _atividadeService.GetById(id);
                var atividadeModel = _mapper.Map<AtividadeGetResponseModel>(atividade);
                return Response<AtividadeGetResponseModel>.Create(atividadeModel);
            });
        }


        /// <summary>
        /// Retorna uma lista de registros
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [SwaggerResponse(200, "Ok", typeof(PagedResponse<IList<AtividadeGetResponseModel>>))]
        [SwaggerResponse(400, "Bad Request", typeof(Response<string>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response<string>))]
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BaseBadRequest("Requisição mal formatada", GetModelStateErros());
            }

            return Execute(() =>
            {
                var atividades = _atividadeService.GetByPessoaIdWithIncludes(GetCurrentUserId(), filter.PageNumber, filter.PageSize, out int totalRecords);
                var atividadesModels = _mapper.Map<IList<Atividade>, IList<AtividadeGetResponseModel>>(atividades);
                return CreatePagedReponse(atividadesModels, filter, totalRecords);
            });
        }
    }
}

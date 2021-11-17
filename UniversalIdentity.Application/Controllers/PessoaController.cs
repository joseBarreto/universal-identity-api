using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Interfaces;
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
        private readonly IBaseService<Pessoa> _baseService;

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="baseService"></param>
        public PessoaController(IBaseService<Pessoa> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Insere um novo registro
        /// </summary>
        /// <param name="pessoa">Modelo para inserir</param>
        /// <returns>Id do obj</returns>
        [SwaggerResponse(200, "Ok", typeof(Response<int>))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        [HttpPost]
        public IActionResult Create([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
                return NotFound();

            return Execute(() => Response<int>.Create(_baseService.Add(pessoa).Id));
        }

    }
}

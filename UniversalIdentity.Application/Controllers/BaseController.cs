using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using UniversalIdentity.Domain.Filter;
using UniversalIdentity.Domain.Wrappers;

namespace UniversalIdentity.Application.Controllers
{
    /// <summary>
    /// Controller base
    /// </summary>
    public class BaseController : Controller
    {

        /// <summary>
        /// Executa uma função garantindo o tratamento de erro
        /// </summary>
        /// <param name="func">função para execução</param>
        /// <returns>
        /// Status code 200 em caso de sucesso
        /// Status code 500 em caso de falha
        /// </returns>
        protected IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response<string>
                {
                    Errors = new string[] {
                        ex.ToString()
                    },
                    Message = "Falha interna no servidor."
                };

                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        internal int GetCurrentUserId()
        {
            _ = int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);
            return userId;
        }

        internal PagedResponse<IList<T>> CreatePagedReponse<T>(IList<T> pagedData, PaginationFilter filter, int totalRecords)
        {
            var respose = new PagedResponse<IList<T>>(pagedData, filter.PageNumber, filter.PageSize);
            var totalPages = ((double)totalRecords / (double)filter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            respose.NextPage =
                filter.PageNumber >= 1 && filter.PageNumber < roundedTotalPages
                ? GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize))
                : null;

            respose.PreviousPage =
                filter.PageNumber - 1 >= 1 && filter.PageNumber <= roundedTotalPages
                ? GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize))
                : null;

            respose.FirstPage = GetPageUri(new PaginationFilter(1, filter.PageSize));
            respose.LastPage = GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize));
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        private Uri GetPageUri(PaginationFilter filter)
        {
            string route = Request.Path.Value;
            var baseUri = string.Concat(Request.Scheme, "://", Request.Host.ToUriComponent());
            var enpointUri = new Uri(string.Concat(baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}

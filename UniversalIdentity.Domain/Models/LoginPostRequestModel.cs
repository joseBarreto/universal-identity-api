using System.ComponentModel.DataAnnotations;

namespace UniversalIdentity.Domain.Models
{
    public class LoginPostRequestModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Número do documento")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string DocumentoNumero { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}

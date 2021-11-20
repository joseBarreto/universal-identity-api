using System.ComponentModel.DataAnnotations;

namespace UniversalIdentity.Domain.Models
{
    public class LoginPostRequestModel
    {
        [EmailAddress(ErrorMessage = "O campo '{0}' não é um endereço de e-mail válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}

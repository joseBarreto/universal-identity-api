using System;
using System.ComponentModel.DataAnnotations;

namespace UniversalIdentity.Domain.Models
{
    public class LoginCreateRequestModel
    {
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        public string Nome { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo '{0}' é obrigatório.")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Enums.Genero Genero { get; set; }

        [Display(Name = "Número do documento")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        public string DocumentoNumero { get; set; }

        [Display(Name = "Tipo do documento")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Enums.DocumentoTipo DocumentoTipo { get; set; }

        [Display(Name = "Data de emissão do documento")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo '{0}' é obrigatório.")]
        public DateTime? DocumentoDataEmissao { get; set; }

        [EmailAddress(ErrorMessage = "O campo '{0}' não é um endereço de e-mail válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Imagem do perfil em base64")]
        [MinLength(1, ErrorMessage = "O campo '{0}' não pode ser nulo ou vazio.")]
        public string ImagemPerfilBase64 { get; set; }

    }
}

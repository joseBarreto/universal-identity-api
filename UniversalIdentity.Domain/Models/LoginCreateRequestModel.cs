using System;
using System.ComponentModel.DataAnnotations;

namespace UniversalIdentity.Domain.Models
{
    public class LoginCreateRequestModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Gênero")]
        public Enums.Genero Genero { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Número do documento")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string DocumentoNumero { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Tipo do documento")]
        public Enums.DocumentoTipo DocumentoTipo { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de emissão do documento")]
        public DateTime? DocumentoDataEmissao { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "E-mail")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Senha")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Senha { get; set; }

    }
}

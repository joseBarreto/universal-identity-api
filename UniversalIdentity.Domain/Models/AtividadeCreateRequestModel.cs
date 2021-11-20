using System;
using System.ComponentModel.DataAnnotations;

namespace UniversalIdentity.Domain.Models
{
    public class AtividadeCreateRequestModel
    {

        [Display(Name = "Título")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        public string Titulo { get; set; }

        [Display(Name = "Local")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        public string Local { get; set; }

        [Display(Name = "Descrição")]
        [MinLength(1, ErrorMessage = "O campo '{0}' não pode ser nulo ou vazio.")]
        public string Descricao { get; set; }

        [Display(Name = "Observação")]
        [MinLength(1, ErrorMessage = "O campo '{0}' não pode ser nulo ou vazio.")]
        public string Observacao { get; set; }

        [Display(Name = "Horas trabalhadas")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public int HorasTrabalhadas { get; set; }

        [Display(Name = "Avaliação")]
        [Range(0.0, 5, ErrorMessage = "O valor para {0} deve está entre {1} e {2}.")]
        public double Avaliacao { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public int PessoaId { get; set; }

    }
}

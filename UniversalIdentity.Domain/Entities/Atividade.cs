using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversalIdentity.Domain.Entities
{
    [Table("T_ATIVIDADE")]
    public class Atividade : BaseEntity
    {
        [Column("DT_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("TITULO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Titulo { get; set; }

        [Column("LOCAL")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Local { get; set; }

        [Column("DESCRICAO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [MinLength(1)]
        public string Descricao { get; set; }

        [Column("OBSERVACAO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public string Observacao { get; set; }

        [Column("HORAS_TRABALHADAS")]
        [Display(Name = "Horas trabalhadas")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public int HorasTrabalhadas { get; set; }

        [Column("AVALIACAO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Avaliação")]
        [Range(0.0, 5, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public double Avaliacao { get; set; }

        [Column("PESSOA_ID")]
        [ForeignKey("T_PESSOA")]
        public int PessoaId { get; set; }

        [Column("AUTOR_ID")]
        [ForeignKey("T_PESSOA")]
        public int? AutorId { get; set; }

        public Pessoa Pessoa { get; set; }

        public Pessoa Autor { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversalIdentity.Domain.Entities
{
    [Table("T_PESSOA")]
    public class Pessoa : BaseEntity
    {
        [Column("STATUS")]
        public bool Status { get; set; }

        [Column("DT_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ATUALIZACAO")]
        public DateTime? DataAtualizacao { get; set; }

        [Column("NOME")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Nome { get; set; }

        [Column("DT_NASCIMENTO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Column("GENERO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Gênero")]
        public Enums.Genero Genero { get; set; }

        [Column("DOCUMENTO_NUMERO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Número do documento")]
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string DocumentoNumero { get; set; }

        [Column("DOCUMENTO_TIPO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Tipo do documento")]
        public Enums.DocumentoTipo DocumentoTipo { get; set; }

        [Column("DOCUMENTO_DT_EMISSAO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de emissão do documento")]
        public DateTime DocumentoDataEmissao { get; set; }

        [Column("TOTAL_AVALIACAO")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Total avaliação")]
        [Range(0.0, 5, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public double TotalAvaliacao { get; set; }

        [Column("TOTAL_HORAS_TRABALHADAS")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Total de horas trabalhadas")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public int TotalHorasTrabalhadas { get; set; }

        [Column("IMAGEM_PERFIL_BASE64")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [MinLength(1)]
        public string ImagemPerfilBase64 { get; set; }

        [Column("UNIVERSAL_ID_BASE64")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [MinLength(1)]
        public string UniversalIdBase64 { get; set; }

        [Column("UNIVERSAL_ID")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(maximumLength: 16, MinimumLength = 16, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string UniversalId { get; set; }
    }
}

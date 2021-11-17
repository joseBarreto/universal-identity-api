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
        public DateTime? DataCadastro { get; set; }

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
        public DateTime? DocumentoDataEmissao { get; set; }
    }
}

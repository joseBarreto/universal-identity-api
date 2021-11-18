using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UniversalIdentity.Domain.Entities
{
    [Table("T_LOGIN")]
    public class Login : BaseEntity
    {
        [Column("PESSOA_ID")]
        public int PessoaId { get; set; }

        [Column("DT_ULTIMO_ACESSO")]
        public DateTime DataUltimoAcesso { get; set; }

        [Column("EMAIL")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "E-mail")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Email { get; set; }

        [Column("SENHA")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Display(Name = "Senha")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "O valor para {0} deve está entre {1} e {2}")]
        public string Senha { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}

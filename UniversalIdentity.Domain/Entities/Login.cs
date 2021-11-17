using System;
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
        public string Email { get; set; }

        [Column("SENHA")]
        public string Senha { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}

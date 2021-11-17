using System.ComponentModel;

namespace UniversalIdentity.Domain.Enums
{
    public enum DocumentoTipo
    {
        [Description("Não definido")]
        Outros = 0,

        [Description("Registro Geral (RG)")]
        RG = 1,

        [Description("Cadastro de Pessoal Física (CPF)")]
        CPF = 2,

        [Description("Carteira Nacional de Habilitação (CNH)")]
        CNH = 2,

        [Description("Passaporte")]
        Passaporte = 3,
    }
}


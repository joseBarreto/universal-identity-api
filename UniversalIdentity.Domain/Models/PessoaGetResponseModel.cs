using System;

namespace UniversalIdentity.Domain.Models
{
    public class PessoaGetResponseModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }
        public Enums.Genero Genero { get; set; }
        public string DocumentoNumero { get; set; }
        public Enums.DocumentoTipo DocumentoTipo { get; set; }
        public DateTime DocumentoDataEmissao { get; set; }
        public string Email { get; set; }
        public double TotalAvaliacao { get; set; }
        public int TotalHorasTrabalhadas { get; set; }
        public string ImagemPerfilBase64 { get; set; }
        public string UniversalIdBase64 { get; set; }
        public string UniversalId { get; set; }

    }
}

using System;

namespace UniversalIdentity.Domain.Models
{

    public class AtividadeGetResponseModel
    {
        public string Titulo { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int HorasTrabalhadas { get; set; }
        public double Avaliacao { get; set; }
        public int AutorId { get; set; }
        public string AutorNome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

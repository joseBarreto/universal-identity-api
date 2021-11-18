using System;

namespace UniversalIdentity.Domain.Models
{
    public class TokenResponseModel
    {
        public PessoaResponseModel Pessoa { get; set; }

        public string Token { get; set; }
        public DateTime ExperireIn { get; set; }

        public class PessoaResponseModel
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }
    }
}

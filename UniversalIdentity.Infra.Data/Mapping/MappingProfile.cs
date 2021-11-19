using AutoMapper;
using UniversalIdentity.Domain.Entities;
using UniversalIdentity.Domain.Models;

namespace UniversalIdentity.Infra.Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Login, LoginCreateRequestModel>()
               .ForMember(dest => dest.DataNascimento, map => map.MapFrom(src => src.Pessoa.DataNascimento))
               .ForMember(dest => dest.DocumentoDataEmissao, map => map.MapFrom(src => src.Pessoa.DocumentoDataEmissao))
               .ForMember(dest => dest.DocumentoNumero, map => map.MapFrom(src => src.Pessoa.DocumentoNumero))
               .ForMember(dest => dest.DocumentoTipo, map => map.MapFrom(src => src.Pessoa.DocumentoTipo))
               .ForMember(dest => dest.Genero, map => map.MapFrom(src => src.Pessoa.Genero))
               .ForMember(dest => dest.Nome, map => map.MapFrom(src => src.Pessoa.Nome))
               .ForMember(dest => dest.ImagemPerfilBase64, map => map.MapFrom(src => src.Pessoa.ImagemPerfilBase64));

            CreateMap<Login, PessoaGetResponseModel>()
              .ForMember(dest => dest.DataNascimento, map => map.MapFrom(src => src.Pessoa.DataNascimento))
              .ForMember(dest => dest.DocumentoDataEmissao, map => map.MapFrom(src => src.Pessoa.DocumentoDataEmissao))
              .ForMember(dest => dest.DocumentoNumero, map => map.MapFrom(src => src.Pessoa.DocumentoNumero))
              .ForMember(dest => dest.DocumentoTipo, map => map.MapFrom(src => src.Pessoa.DocumentoTipo))
              .ForMember(dest => dest.Genero, map => map.MapFrom(src => src.Pessoa.Genero))
              .ForMember(dest => dest.Nome, map => map.MapFrom(src => src.Pessoa.Nome))
              .ForMember(dest => dest.TotalAvaliacao, map => map.MapFrom(src => src.Pessoa.TotalAvaliacao))
              .ForMember(dest => dest.TotalHorasTrabalhadas, map => map.MapFrom(src => src.Pessoa.TotalHorasTrabalhadas))
              .ForMember(dest => dest.ImagemPerfilBase64, map => map.MapFrom(src => src.Pessoa.ImagemPerfilBase64))
              .ForMember(dest => dest.UniversalIdBase64, map => map.MapFrom(src => src.Pessoa.UniversalIdBase64))
              .ForMember(dest => dest.UniversalId, map => map.MapFrom(src => src.Pessoa.UniversalId));

            CreateMap<LoginCreateRequestModel, Pessoa>();

            CreateMap<LoginCreateRequestModel, Login>()
                .ForMember(dest => dest.Pessoa, map => map.MapFrom(src => src));
        }
    }
}

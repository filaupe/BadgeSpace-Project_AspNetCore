using AutoMapper;
using Domain.Argumentos.Estudante;
using Domain.Argumentos.Usuario;
using Domain.Argumentos.Usuario.Requests;
using Domain.Entidades.Estudante;
using Domain.Entidades.Usuario;

namespace Infra.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper() 
        { 
            CreateMap<Usuario, UsuarioResponse>(); 
            CreateMap<Usuario, UsuarioEmail>(); 
            CreateMap<Usuario, UsuarioToken>(); 
            CreateMap<Estudante, EstudanteResponse>(); 
        }
    }
}

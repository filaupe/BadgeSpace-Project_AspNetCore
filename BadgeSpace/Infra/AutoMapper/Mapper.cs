using AutoMapper;
using Domain.Argumentos.Estudante;
using Domain.Argumentos.Usuario;
using Domain.Entidades.Estudante;
using Domain.Entidades.Usuario;

namespace Infra.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper() 
        { 
            CreateMap<Usuario, UsuarioResponse>(); 
            CreateMap<Estudante, EstudanteResponse>(); 
        }
    }
}

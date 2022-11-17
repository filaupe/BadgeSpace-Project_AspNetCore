using AutoMapper;
using Domain_Driven_Design.Domain.Argumentos.Estudante;
using Domain_Driven_Design.Domain.Argumentos.Usuario;
using Domain_Driven_Design.Domain.Argumentos.Usuario.Requests;
using Domain_Driven_Design.Domain.Entidades.Estudante;
using Domain_Driven_Design.Domain.Entidades.Usuario;

namespace Domain_Driven_Design.Infra.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper() 
        { 
            CreateMap<Usuario, UsuarioResponse>(); 
            CreateMap<Usuario, UsuarioEmail>(); 
            CreateMap<Usuario, UsuarioToken>(); 
            CreateMap<Estudante, EstudanteResponse>(); 
            CreateMap<Estudante, EstudanteRequest>(); 
        }
    }
}

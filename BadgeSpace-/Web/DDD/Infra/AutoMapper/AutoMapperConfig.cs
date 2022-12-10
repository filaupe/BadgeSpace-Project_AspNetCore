using AutoMapper;

namespace Domain_Driven_Design.Infra.AutoMapper
{
    public class AutoMapperConfig
    {
        public MapperConfiguration Configure() => new (cfg => { cfg.AddProfile<Mapper>(); });
    }
}

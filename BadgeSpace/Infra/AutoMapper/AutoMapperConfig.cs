using AutoMapper;

namespace Infra.AutoMapper
{
    public class AutoMapperConfig
    {
        public MapperConfiguration Configure() => new (cfg => { cfg.AddProfile<Mapper>(); });
    }
}

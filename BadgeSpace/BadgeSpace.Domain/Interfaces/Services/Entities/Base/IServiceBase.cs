namespace BadgeSpace.Domain.Interfaces.Services.Entities.Base
{
    public interface IServiceBase<TEntity, in TId> 
        where TEntity : class 
        where TId : struct
    {

    }
}

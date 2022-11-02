namespace Web.Utils.MethodsExtensions.AddMethods.Interfaces
{
    public interface IMethods : IMethodGet, IMethodPost, IMethodPut, IMethodDelete
    {
        public object getMessage(string message);
    }
}

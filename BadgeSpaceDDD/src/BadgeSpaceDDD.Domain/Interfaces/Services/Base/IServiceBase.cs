using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Base
{
    internal interface IServiceBase
    {
        public interface IServiceBase : INotifiable, IDisposable { }
    }
}

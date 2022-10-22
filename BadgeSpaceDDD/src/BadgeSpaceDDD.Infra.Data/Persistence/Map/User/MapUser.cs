using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence.Map.User
{
    internal class MapUser : EntityTypeConfiguration<Domain.Entities.User.User>
    {
        public MapUser()
        {

        }
    }
}

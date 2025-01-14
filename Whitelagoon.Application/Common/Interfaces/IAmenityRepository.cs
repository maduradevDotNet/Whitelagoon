using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whitelagoon.Domain.Entities;

namespace Whitelagoon.Application.Common.Interfaces
{
    public interface IAmenityRepository:IRepository<Amenity>
    {
        void Update(Amenity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whitelagoon.Domain.Entities;

namespace Whitelagoon.Application.Common.Interfaces
{
    public interface IIVillaNumberRepository:IRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
    }
}

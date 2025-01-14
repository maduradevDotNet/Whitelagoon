using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whitelagoon.Application.Common.Interfaces;
using Whitelagoon.Domain.Entities;
using Whitelagoon.Infrastructure.Data;

namespace Whitelagoon.Infrastructure.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IIVillaNumberRepository
    {
        private readonly ApplicationDBContext _db;

        public VillaNumberRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(VillaNumber entity)
        {
            _db.VillaNumbers.Update(entity);
        }
    }
}

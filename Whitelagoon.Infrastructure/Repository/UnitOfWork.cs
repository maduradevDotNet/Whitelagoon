using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whitelagoon.Application.Common.Interfaces;
using Whitelagoon.Infrastructure.Data;

namespace Whitelagoon.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _db; 
        public IVillaRepository Villa { get; private set; }

        public IIVillaNumberRepository VillaNumber { get; private set; }

        public IAmenityRepository Amenity { get; private set; }

        public UnitOfWork(ApplicationDBContext db) { 
            _db = db;
            Villa=new VillaRepository(_db);
            VillaNumber=new VillaNumberRepository(_db);
            Amenity = new AmenityRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

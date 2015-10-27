using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.InMemory
{
    public class FlagRepository : IFlagRepository
    {
        public FlagRepository()
        {
            _context = new List<Flag>()
            {
                new Flag() {
                    Id =Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = new Guid(),
                    Reason = "Innapropriate"
                }
            };
        }
        private List<Flag> _context;

        public Flag CreateFlag(Flag newFlag)
        {
            newFlag.Id = Guid.NewGuid();
            _context.Add(newFlag);
            return newFlag;
        }

        public IEnumerable<Flag> GetAll()
        {
            return _context;
        }

        public Flag GetById(Guid flagId)
        {
            return _context.FirstOrDefault(f => f.Id == flagId);
        }

        public bool DeleteFlag(Guid flagId)
        { 
            var flag = _context.FirstOrDefault(f=>f.Id==flagId);
            if (flag == null)
                return false;
            return _context.Remove(flag);
        }



    }
}
